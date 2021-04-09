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
        IDataResult<List<OperationClaim>> GetDefaultClaims(string defaultClaim);
        IResult Add(OperationClaim operationClaim);
        IResult Delete(string name);
        IResult Update(OperationClaim operationClaim);
        IResult CheckIfClaimExist(int operationClaimId);
        IResult CheckIfClaimsExist();

    }
}
