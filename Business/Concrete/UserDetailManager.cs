
using System.Collections.Generic;
using Business.Abstruct;
using Business.Constant;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;
using System.Linq;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete
{
    class UserDetailManager : IUserDetailService
    {
        private readonly IUserDetailDal _userDetailDal;

        public UserDetailManager(IUserDetailDal userDetailDal)
        {
            _userDetailDal = userDetailDal;
        }

        [SecuredOperation("User/Root")]
        public IDataResult<List<UserDetail>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailsExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<UserDetail>>(result.Message);
            }

            return new SuccessDataResult<List<UserDetail>>(_userDetailDal.GetAll());
        }

        private IResult CheckIfUserDetailsExist()
        {
            var result = _userDetailDal.GetAll().Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(UserDetailMessages.UsersDoNotExist);
        }

        [SecuredOperation("User/Root")]
        public IResult Add(UserDetail userDetail)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailAlreadyExist(userDetail.Id)
                );
            if (result != null)
            {
                return result;
            }

            _userDetailDal.Add(userDetail);
            return new SuccessResult();
        }

        private IResult CheckIfUserDetailAlreadyExist(int userDetailId)
        {
            var result = _userDetailDal.GetAll(ud => ud.Id == userDetailId).Any();

            if (result)
            {
                return new ErrorResult(UserDetailMessages.UserDetailAlreadyExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailAlreadyDeleted(id)
                );
            if (result != null)
            {
                return result;
            }

            var deleteToUserDetail = _userDetailDal.Get(ud => ud.Id == id);
            _userDetailDal.Delete(deleteToUserDetail);
            return new SuccessResult();
        }

        private IResult CheckIfUserDetailAlreadyDeleted(int userDetailId)
        {
            var result = _userDetailDal.GetAll(ud => ud.Id == userDetailId).Any();

            if (result)
            {
                return new ErrorResult(UserDetailMessages.UserDetailAlreadyDeleted);
            }

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Update(UserDetail userDetail)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailExist(userDetail.Id)
                );
            if (result != null)
            {
                return result;
            }

            _userDetailDal.Update(userDetail);
            return new SuccessResult();
        }

        private IResult CheckIfUserDetailExist(int userDetailId)
        {
            var result = _userDetailDal.GetAll(ud => ud.Id == userDetailId).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(UserDetailMessages.UserDetailDoNotExist);
        }

        [SecuredOperation("Root")]
        public IDataResult<UserDetail> GetById(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<UserDetail>(result.Message);
            }

            return new SuccessDataResult<UserDetail>(_userDetailDal.Get(ud => ud.Id == id));
        }
    }

    
}
