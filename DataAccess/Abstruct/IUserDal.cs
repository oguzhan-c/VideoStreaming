using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAcces;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace DataAccess.Abstruct
{
    public interface IUserDal : IEntityRepository<User>
    {
        IDataResult<List<OperationClaim>> GetClaimsByUser(int userId);
        IDataResult<User> GetUserForRegister(User user);
    }
}
