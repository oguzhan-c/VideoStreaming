using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAcces.EntitiyFramework;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;
using DataAccess.Abstruct;
using DataAccess.Concrete.EntityFremavork.DatabaseContexts;
using System.Linq;
using Core.Utilities.Results.Concrute;

namespace DataAccess.Concrete.EntityFremavork
{
    public class EfOperationClaimDal : EfEntitiyRepositoryBase<OperationClaim, VideoStreamingContext>, IOperationClaimDal
    {
        //Eklenen her kullanıcıya default olarak user claim ini atamak için kullanıclacak.z
        public IDataResult<List<OperationClaim>> GetDefaultClaims(string defaultClaim)
        {
            using (var context = new VideoStreamingContext())
            {
                var result = from operationClaim in context.OperationClaims
                    where operationClaim.ClaimType == defaultClaim
                    select new OperationClaim {Id = operationClaim.Id , Name = operationClaim.Name , Date = operationClaim.Date};

                return new SuccessDataResult<List<OperationClaim>>(result.ToList());
            }
        }
    }
}
