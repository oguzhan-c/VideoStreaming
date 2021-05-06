using System.Collections.Generic;
using Business.Abstruct;
using Core.Entities.Concrute;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using System.Linq;
using Business.BusinessAspects.Autofac;
using Business.Constant;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        private readonly IUserService _userService;//Kullanıcı olup olmadığı kontrolü sağlanması için gerekli.

        public OperationClaimManager(IOperationClaimDal operationClaimDal , IUserService userService)
        {
            _operationClaimDal = operationClaimDal;
            _userService = userService;
        }

        [SecuredOperation("Root")]
        public IDataResult<List<OperationClaim>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfClaimsExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<OperationClaim>>(result.Message);
            }

            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        public IResult CheckIfClaimsExist()
        {
            var result = _operationClaimDal.GetAll().Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(OperationClaimMessages.ThisClaimsDoNotExist);
        }

        [SecuredOperation("Root")]
        public IDataResult<OperationClaim> GetById(int id)
        {
            IResult result = BusinessRule.Run
            (
                CheckIfClaimExist(id)
            );

            if (result != null)
            {
                return new ErrorDataResult<OperationClaim>(result.Message);
            }

            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(op => op.Id == id));
        }

        public IDataResult<List<OperationClaim>> GetDefaultClaims(string defaultClaim)
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetDefaultClaims(defaultClaim).Data);
        }                                                                                                                                                                                                           

        [SecuredOperation("Root")]
        public IDataResult<List<OperationClaim>> GetByUser(int userId)
        {
            var result = _userService.GetClaims(userId);

            if (result.Succcess)
            {
                return new SuccessDataResult<List<OperationClaim>>(result.Data);
            }

            return new ErrorDataResult<List<OperationClaim>>(result.Message);
        }

        [SecuredOperation("Root")]
        public IResult Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRule.Run
            (
                CheckIfClaimAlreadyExist(operationClaim.Name)
            );

            if (result != null)
            {
                return result;
            }

            _operationClaimDal.Add(operationClaim);
            return new SuccessResult();
        }

        public IResult CheckIfClaimAlreadyExist(string operationClaimName)
        {
            var result = _operationClaimDal.GetAll(op => op.Name == operationClaimName).Any();
            if (result)
            {
                return new ErrorResult(OperationClaimMessages.ThisClaimAlreadyExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation("Root")]
        public IResult Delete(string name)
        {
            var deleteToOperationClaim = _operationClaimDal.Get(op => op.Name == name);
            IResult result = BusinessRule.Run
            (
                CheckIfClaimAlreadyDeleted(deleteToOperationClaim.Name)
            );

            if (result != null)
            {
                return result;
            }

            _operationClaimDal.Delete(deleteToOperationClaim);
            return new SuccessResult(OperationClaimMessages.Deleted);
        }

        private IResult CheckIfClaimAlreadyDeleted(string name)
        {
            var result = _operationClaimDal.GetAll(op=>op.Name == name).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(OperationClaimMessages.ThisClaimAlreadyDeleted);
        }

        [SecuredOperation("Root")]
        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRule.Run
            (
                CheckIfClaimExist(operationClaim.Id)
            );

            if (result != null)
            {
                return result;
            }
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(OperationClaimMessages.Updated);
        }

        public IResult CheckIfClaimExist(int operationClaimId)
        {
            var result = _operationClaimDal.GetAll(op=>op.Id == operationClaimId).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(OperationClaimMessages.ThisClaimDoNotExist);

        }
    }
}
