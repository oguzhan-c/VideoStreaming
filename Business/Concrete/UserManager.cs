using System.Collections.Generic;
using System.Linq;
using Business.Abstruct;
using Business.Constant;
using Core.Entities.Concrute;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            IResult result = BusinessRule.Run
            (
                CheckIfUserAlreadyExist(user.Email)
            );

            if (result != null)
            {
                return result;
            }

            _userDal.Add(user);

            return new SuccessResult(UserMessages.UserAdded);
        }

        public IResult CheckIfUserAlreadyExist(string email)
        {
            var result = _userDal.GetAll(u => u.Email == email).Any();
            if (result)
            {
                return new ErrorResult(UserMessages.UserAlreadyExist);
            }

            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var deleteToUser = _userDal.Get(u => u.Id == id);

            IResult result = BusinessRule.Run
            (
                CheckIfUserAlreadyExist(deleteToUser.Email),
            CheckIfUserAlreadyDeleted(id)
            );

            if (result != null)
            {
                return result;
            }

            _userDal.Delete(deleteToUser);

            return new SuccessResult();
        }

        private IResult CheckIfUserAlreadyDeleted(int id)
        {
            var result = _userDal.GetAll(u => u.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(UserMessages.UserAlreadyDeleted);

        }

        public IDataResult<List<User>> GetAll()
        {
            IResult result = BusinessRule.Run
            (
                CheckIfUsersExist()
            );

            if (result != null)
            {
                return new ErrorDataResult<List<User>>(result.Message);
            }

            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IResult CheckIfUsersExist()
        {
            var result = _userDal.GetAll().Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(UserMessages.UsersDoNotExist);
        }

        public IDataResult<User> GetById(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<User>(result.Message);
            }

            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }

        public IDataResult<List<OperationClaim>> GetClaims(int userId)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserExist(userId)
                );
            if (result != null)
            {
                return new ErrorDataResult<List<OperationClaim>>(result.Message);
            }

            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaimsByUser(userId).Data);
        }

        public IDataResult<User> GetUserForRegister(User user)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserExist(user.Id)
                );
            if (result != null)
            {
                return new ErrorDataResult<User>(result.Message);
            }

            return new SuccessDataResult<User>(_userDal.GetUserForRegister(user).Data);
        }

        public IResult CheckIfUserExist(int id)
        {
            var result = _userDal.GetAll(u => u.Id == id).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(UserMessages.UserDoNotExist);
        }

        public IDataResult<User> GetByMail(string email)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserAlreadyExist(email)
                );
            if (result != null)
            {
                return new ErrorDataResult<User>(result.Message);
            }

            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IResult Update(User user)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserExist(user.Id)
                );
            if (result != null)
            {
                return result;
            }

            return new SuccessResult(UserMessages.UserUpdated);
        }
    }
}
