using System.Collections.Generic;
using Business.Abstruct;
using Core.Entities.Concrute;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using System.Linq;
using Business.Constant;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IUserService _userService;
        private readonly IOperationClaimService _operationClaimService;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IUserService userService, IOperationClaimService operationClaimService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _userService = userService;
            _operationClaimService = operationClaimService;
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                _userService.CheckIfUsersExist(),
                    _operationClaimService.CheckIfClaimsExist(),
                    CheckIfUserOperationClaimsExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<UserOperationClaim>>(result.Message);
            }

            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll());
        }

        private IResult CheckIfUserOperationClaimsExist()
        {
            var result = _userOperationClaimDal.GetAll().Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(UserOperationClaimMessages.ThisUserOperationClaimsDoNotExist);
        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {
            var selectedUserOperationClaim = _userOperationClaimDal.Get(uoc => uoc.Id == id);
            IResult result = BusinessRule.Run
                ( _userService.CheckIfUserExist(selectedUserOperationClaim.UserId),
                    _operationClaimService.CheckIfClaimExist(selectedUserOperationClaim.OperationClaimId),
                    CheckIfUserOperationClaimExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<UserOperationClaim>(result.Message);
            }

            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(uoc => uoc.Id == id));
        }

        private IResult CheckIfUserOperationClaimExist(int id)
        {
            var result = _userOperationClaimDal.GetAll(uoc => uoc.Id == id).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(UserOperationClaimMessages.ThisUserOperationClaimDoNotExist);
        }

        public IResult Add(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserOperationClaimExist(userOperationClaim.Id)
                );
            if (result == null)
            {
                return new ErrorResult(UserOperationClaimMessages.ThisUserOperationClaimAlreadyExist);
            }

            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult();
        }

        public IResult Update(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserOperationClaimExist(userOperationClaim.Id)
                );
            if (result != null)
            {
                return result;
            }

            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var deleteToUSerOperationClaim = _userOperationClaimDal.Get(uoc => uoc.Id == id);
            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUserExist(deleteToUSerOperationClaim.UserId),
                    _operationClaimService.CheckIfClaimExist(deleteToUSerOperationClaim.OperationClaimId),
                    CheckIfUserOperationClaimExist(deleteToUSerOperationClaim.Id)
                );
            if (result != null)
            {
                return new ErrorResult(UserOperationClaimMessages.ThisUserOperationClaimAlreadyDeleted);
            }

            _userOperationClaimDal.Delete(deleteToUSerOperationClaim);
            return new SuccessResult();
        }

        public IDataResult<List<OperationClaim>> GetByUser(int userId)
        {
            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUserExist(userId)
                );
            if (result != null)
            {
                return new ErrorDataResult<List<OperationClaim>>(result.Message);
            }

            return new SuccessDataResult<List<OperationClaim>>(_userService.GetClaims(userId).Data);
        }
    }
}
