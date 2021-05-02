using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstruct
{
    public interface IUserDetailService
    {
        IDataResult<List<UserDetail>> GetAll();
        IResult Add(UserDetail userDetail);
        IResult AddProfilePhoto(IFormFile photoFile, int id);
        IResult Delete(int id);
        IResult Update(UserDetail userDetail);
        IDataResult<UserDetail> GetById(int id);
        IDataResult<UserDetail> GetByUserId(int userId);
    }
}
