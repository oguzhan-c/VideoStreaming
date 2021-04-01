using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAcces.EntitiyFramework;
using Core.Entities.Concrute;
using DataAccess.Abstruct;
using DataAccess.Concrete.EntityFremavork.DatabaseContexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFremavork
{
    public class EfCommentDal : EfEntitiyRepositoryBase<Comment, VideStreamingContext>, ICommentDal
    {

    }
}
