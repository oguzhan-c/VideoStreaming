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
        //Gönderilen kullanıcıya göre ona ait olan yetkileri liste olarak geriye döndürür.
        public IDataResult<List<OperationClaim>> GetClaimsByUser(int userId)
        {
            using (var context = new VideoStreamingContext())
            {
                var result = from operationClaim in context.OperationClaims
                    join userOperationClaim in context.UserOperationClaims
                        on operationClaim.Id equals userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == userId
                    select new OperationClaim {Id = operationClaim.Id, Name = operationClaim.Name};

                return new SuccessDataResult<List<OperationClaim>>(result.ToList());
            }
        }

        //Kullanıcı eklendikten sonra geriye kalan alanların eklenmesi için user.id gerekli
        //Bu method sayesinde gerekli olan user.id getiriliyor.
        public IDataResult<User> GetUserForRegister(User user)
        {
            using (var context = new VideoStreamingContext())
            {
                var result = from dbUser in context.Users
                    where dbUser.Email == user.Email
                    select new User
                    {
                        Id = dbUser.Id,
                        Email = dbUser.Email,
                        FirstName = dbUser.FirstName,
                        LastName = dbUser.LastName,
                        PasswordHash = dbUser.PasswordHash,
                        PasswordSalt = dbUser.PasswordSalt,
                        Status = dbUser.Status
                    };
                var users = result.ToArray();

                return new SuccessDataResult<User>(users[0]);
            }
        }
    }
}
