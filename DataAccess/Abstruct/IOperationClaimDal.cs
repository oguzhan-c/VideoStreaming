using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAcces;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;

namespace DataAccess.Abstruct
{
    public interface IOperationClaimDal : IEntityRepository<OperationClaim>
    {
        IDataResult<List<OperationClaim>> GetDefaultClaims(string defaultOperationClaim);
    }
}
