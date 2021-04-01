using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAcces;
using Core.Entities.Concrute;
using Entities.Concrete;

namespace DataAccess.Abstruct
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
