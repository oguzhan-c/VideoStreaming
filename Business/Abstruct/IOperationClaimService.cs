using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;

namespace Business.Abstruct
{
    public interface IOperationClaimService
    {
        IDataResult<List<OperationClaim>> GetAll();
        IDataResult<OperationClaim> GetById(int id);
        IDataResult<List<OperationClaim>> GetByUser(User user);
        IResult Add(OperationClaim operationClaim);
        IResult Delete(string name);
        IResult Update(OperationClaim operationClaim);
        IResult ChackIfClaimExist(int operationClaimId);
        IResult ChackIfClaimsExist();

    }
}
