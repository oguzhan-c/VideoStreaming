using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAcces;
using Entities.Concrete;

namespace DataAccess.Abstruct
{
    public interface IAuthDal : IEntityRepository<JasonWebToken>
    {
    }
}
