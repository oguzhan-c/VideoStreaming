using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAcces.EntitiyFramework;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;
using DataAccess.Abstruct;
using DataAccess.Concrete.EntityFremavork.DatabaseContexts;
using System.Linq;


namespace DataAccess.Concrete.EntityFremavork
{
    public class EfUserOperationClaimDal : EfEntitiyRepositoryBase<UserOperationClaim, VideoStreamingContext>, IUserOperationClaimDal
    {
    }
}
