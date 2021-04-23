using System;
using Business.Abstruct;
using Business.Constant;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
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

        public AuthManager(ITokenHelper tokenHelper, IUserService userService, ICommunicationService communicationService, IUserDetailService userDetailService, IOperationClaimService operationClaimService, IUserOperationClaimService userOperationClaimService, IChannelService channelService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
            _communicationService = communicationService;
            _userDetailService = userDetailService;
            _operationClaimService = operationClaimService;
            _userOperationClaimService = userOperationClaimService;
            _channelService = channelService;
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
                Status = true,//Şimdilik direkt olarak sitede oturum açabilecek daha sonra email doğrulama modülü eklenecek
            };
            
            var communication = new Communication
            {
                UserId = _userService.GetUserForRegister(user).Data.Id,
                Address1 = userForRegisterDto.Address1,

                Address2 = userForRegisterDto.Address2,
                City = userForRegisterDto.City,
                Continent = userForRegisterDto.Continent,
                Country = userForRegisterDto.Country,
                PhoneNumber = userForRegisterDto.PhoneNumber,
                Street = userForRegisterDto.Street,
                ZipCode = userForRegisterDto.ZipCode,
            };

            var userDetail = new UserDetail
            {
                UserId = _userService.GetUserForRegister(user).Data.Id,
                DateOfBorn = userForRegisterDto.DateOfBorn,
                DateOfJoin = DateTime.Now,//Direkt kayıt olduğu zaman atanacak
                Gender = userForRegisterDto.Gender,
                IdentityNumber = userForRegisterDto.IdentityNumber,
                RecoveryEmail = userForRegisterDto.RecoveryEmail
            };

            var channel = new Channel
            {
                UserId = user.Id,
                ChannelName = userForRegisterDto.ChannelName,
                ChannelPhotoPath = null,
                Description = null,
                InstallationDate = userDetail.DateOfJoin,
                UpdateDate = DateTime.Now
            };

            _userService.Add(user);
            _communicationService.Add(communication);
            _userDetailService.Add(userDetail);
            _userService.Add(user);
            _channelService.Add(channel);

            //Register olduktan sonra kullanıcıya default olarak operationClaim.ClaimType da Default olarak belirtilen
            //ilk claim atanıyor.
            var userOperationClaim = new UserOperationClaim
            {
                UserId = user.Id,
                OperationClaimId = _operationClaimService.GetDefaultClaims("Default").Data[0].Id
            };

            _userOperationClaimService.Add(userOperationClaim);

            return new SuccessDataResult<User>(user);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (!userToCheck.Succcess)
            {
                return new ErrorDataResult<User>(userToCheck.Message);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(UserMessages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck.Data, UserMessages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            var result = _userService.CheckIfUserAlreadyExist(email);

            if (!result.Succcess)
            {
                return new ErrorResult(UserMessages.UserAlreadyExist);
            }

            return new SuccessResult();

        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user.Id);
            var accessToken = _tokenHelper.createToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, UserMessages.AccessTokenCreated);
        }
    }
}
