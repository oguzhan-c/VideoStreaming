using System;
using System.Linq;
using Business.Abstruct;
using Business.Constant;
using Core.Entities.Concrute;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstruct;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {

        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICommunicationService _communicationService;
        private readonly IUserDetailService _userDetailService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IChannelService _channelService;
        private readonly IAuthDal _authDal;

        public AuthManager(ITokenHelper tokenHelper, IUserService userService, ICommunicationService communicationService, IUserDetailService userDetailService, IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService, IChannelService channelService, IAuthDal authDal)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
            _communicationService = communicationService;
            _userDetailService = userDetailService;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
            _channelService = channelService;
            _authDal = authDal;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            HashingHelper.CreatepasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,//Buradaki PasswordHash Ve PasswordSalt yukarıda Hashhing Helper da out keywordleri
                PasswordSalt = passwordSalt,//ile verlilen passwordsalt ve password hash. out keyword ü ile verilen obje geriye döndürülür.
                Status = true//Şimdilik direkt olarak onay verildi.Daha sonra EMail Onay Modülü Eklenecek.
            };

            _userService.Add(user);

            var communication = new Communication
            {
                UserId = user.Id,
                Address1 = userForRegisterDto.Address1,
                Address2 = userForRegisterDto.Address2,
                City = userForRegisterDto.City,
                Continent = userForRegisterDto.Continent,
                Country = userForRegisterDto.Country,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Street = userForRegisterDto.Street,
                ZipCode = userForRegisterDto.ZipCode,
            };
            _communicationService.Add(communication);

            var userDetail = new UserDetail
            {
                UserId = user.Id,
                DateOfBorn = userForRegisterDto.DateOfBorn,
                DateOfJoin = DateTime.Now,//Direkt kayıt olduğu zaman atanacak
                Gender = userForRegisterDto.Gender,
                IdentityNumber = userForRegisterDto.IdentityNumber,
                RecoveryEmail = userForRegisterDto.RecoveryEmail
            };
            _userDetailService.Add(userDetail);

            var channel = new Channel
            {
                UserId = user.Id,
                ChannelName = $"{user.FirstName} {user.LastName}",
                ChannelPhotoPath = "a",
                InstallationDate = userDetail.DateOfJoin,
                Description = $"This Channel Owner Name is {user.FirstName} {user.LastName}.This Channel Build on {userDetail.DateOfJoin}",
                UpdateDate = DateTime.Now
            };
            _channelService.Add(channel);

            //Register olduktan sonra kullanıcıya default olarak operationClaim.ClaimType da Default olarak belirtilen
            //ilk claim atanıyor.
            var userOperationClaim = new UserOperationClaim
            {
                UserId = user.Id,
                OperationClaimId = _operationClaimService.GetDefaultClaims("User").Data[0].Id
            };

            _userOperationClaimService.Add(userOperationClaim);

            return new SuccessDataResult<User>(user);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {

            IResult result = BusinessRule.Run
                (
                    CheckIfVerifyAccount(userForLoginDto.Email)
                );

            if (result != null)
            {
                return new ErrorDataResult<User>(result.Message);
            }

            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (!userToCheck.Succcess)
            {
                return new ErrorDataResult<User>(userToCheck.Message);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash,
                userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(UserMessages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, UserMessages.SuccessfulLogin);
        }

        public IResult LogOut(int userId)
        {
            var results = _authDal.GetAll(jwt=>jwt.UserId == userId);

            foreach (var result in results)
            {
                _authDal.Delete(result);
            }

            return new SuccessResult();

        }

        private IResult CheckIfVerifyAccount(string email)
        {
            var result = _userService.GetByMail(email).Data;

            if (result.Status == false)
            {
                return new ErrorResult(AuthMessages.ThisEmailDoesNotVerify);
            }

            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user.Id);
            var accessToken = _tokenHelper.createToken(user, claims.Data);

            var jwt = new JasonWebToken
            {
                UserId = user.Id,
                Token = accessToken.Token,
                Expiration = accessToken.Expiration
            };

            _authDal.Add(jwt);

            return new SuccessDataResult<AccessToken>(accessToken, UserMessages.AccessTokenCreated);
        }
    }
}
