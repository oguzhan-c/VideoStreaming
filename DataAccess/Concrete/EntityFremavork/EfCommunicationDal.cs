using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAcces.EntitiyFramework;
using DataAccess.Abstruct;
using DataAccess.Concrete.EntityFremavork.DatabaseContexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFremavork
{
    public class EfCommunicationDal : EfEntitiyRepositoryBase<Communication, VideoStreamingContext> , ICommunicationDal
    {
    }
}
