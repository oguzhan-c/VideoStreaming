using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstruct;
using Business.Constat;
using Core.Entities.Concrute;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Microsoft.EntityFrameworkCore.Internal;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private IOperationClaimDal _operationClaimDal;
        private IUserDal _userDal;//Claimleri hangi kullanıcı için getiriceğimizi söyleyen method orada  olduğu için gerekli.
        private IUserService _userService;//Kullanıcı olup olmadığı kontrolü sağlanması için gerekli.

        public OperationClaimManager(IOperationClaimDal operationClaimDal, IUserDal userDal, IUserService userService)
        {
            _operationClaimDal = operationClaimDal;
            _userDal = userDal;
            _userService = userService;
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    ChackIfClaimsExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<OperationClaim>>(result.Message);
            }

            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        public IResult ChackIfClaimsExist()
        {
            var result = _operationClaimDal.GetAll().Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(OperationClaimMessages.ThisClaimsDoNotExist);
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            IResult result = BusinessRule.Run
            (
                ChackIfClaimExist(id)
            );

            if (result != null)
            {
                return new ErrorDataResult<OperationClaim>(result.Message);
            }

            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(op => op.Id == id));
        }

        public IDataResult<List<OperationClaim>> GetByUser(User user)
        {
            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUserExist(user.Id)
                );

            if (result != null)
            {
                return new ErrorDataResult<List<OperationClaim>>(result.Message);
            }

            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user).Data);
        }

        public IResult Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRule.Run
            (
                ChackIfClaimAlreadyExist(operationClaim.Name)
            );

            if (result != null)
            {
                return result;
            }

            _operationClaimDal.Add(operationClaim);
            return new SuccessResult();
        }

        public IResult ChackIfClaimAlreadyExist(string operationClaimName)
        {
            var result = _operationClaimDal.GetAll(op => op.Name == operationClaimName).Any();
            if (result)
            {
                return new ErrorResult(OperationClaimMessages.ThisClaimAlreadyExist);
            }

            return new SuccessResult();
        }

        public IResult Delete(string name)
        {
            var deleteToOperationClaim = _operationClaimDal.Get(op => op.Name == name);
            IResult result = BusinessRule.Run
            (
                ChackIfClaimAlreadyDeleted(deleteToOperationClaim.Name)
            );

            _operationClaimDal.Delete(deleteToOperationClaim);
            return new SuccessResult(OperationClaimMessages.Deleted);
        }

        private IResult ChackIfClaimAlreadyDeleted(string name)
        {
            var result = _operationClaimDal.GetAll(op=>op.Name == name).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(OperationClaimMessages.ThisClaimAlreadyDeleted);
        }


        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRule.Run
            (
                ChackIfClaimExist(operationClaim.Id)
            );

            if (result != null)
            {
                return result;
            }
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(OperationClaimMessages.Updated);
        }

        public IResult ChackIfClaimExist(int operationClaimId)
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
