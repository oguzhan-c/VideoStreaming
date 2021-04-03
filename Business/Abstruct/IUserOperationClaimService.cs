using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;

namespace Business.Abstruct
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<UserOperationClaim>> GetAll();
        IDataResult<UserOperationClaim> GetById(int id);
        IResult Add(UserOperationClaim userOperationClaim);
        IResult Update(UserOperationClaim userOperationClaim);
        IResult Delete(int id);

    }
}
