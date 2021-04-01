using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface IUserDetailService
    {
        IDataResult<List<UserDetail>> GetAll();
        IResult Add(UserDetail userDetail);
        IResult Delete(UserDetail userDetail);
        IResult Update(UserDetail userDetail);
        IDataResult<UserDetail> GetById(int id);
    }
}
