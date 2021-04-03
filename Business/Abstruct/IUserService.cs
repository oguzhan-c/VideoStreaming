using System;
using System.Collections.Generic;
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
        IResult CheckIfUserExist(int id);
        IResult CheckIfUsersExist();

    }
}
