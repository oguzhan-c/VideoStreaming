using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAcces.EntitiyFramework;
using Core.Entities.Concrute;
using DataAccess.Abstruct;
using DataAccess.Concrete.EntityFremavork.DatabaseContexts;

namespace DataAccess.Concrete.EntityFremavork
{
    public class EfUserOperationClaimDal : EfEntitiyRepositoryBase<UserOperationClaim, VideStreamingContext>, IUserOperationClaimDal
    {

    }
}
