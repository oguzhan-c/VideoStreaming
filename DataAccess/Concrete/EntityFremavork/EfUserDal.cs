using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAcces.EntitiyFramework;
using Core.Entities.Concrute;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using DataAccess.Concrete.EntityFremavork.DatabaseContexts;

namespace DataAccess.Concrete.EntityFremavork
{
    public class EfUserDal : EfEntitiyRepositoryBase<User, VideoStreamingContext>, IUserDal
    {
        //Gönderilen kullanıcıya göre ona ait olan yetkileri listeolarak geriye döndürür.
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var context = new VideoStreamingContext();
            var result = from operationClaim in context.OperationClaims
                join userOperationClaim in context.UserOperationClaims
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == user.Id
                select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

            return new SuccessDataResult<List<OperationClaim>>(result.ToList());
        }
    }
}
