using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;

namespace Business.Abstruct
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IResult Add(User user);
        IResult Delete(int id);
        IResult Update(User user);
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByMail(string email);
        IDataResult<List<OperationClaim>> GetClaims(int userId);
        IDataResult<User> GetUserForRegister(User user);
        IResult CheckIfUserExist(int id);
        IResult CheckIfUsersExist();
        IResult CheckIfUserAlreadyExist(string email);
    }
}
