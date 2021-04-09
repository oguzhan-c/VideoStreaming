using System.Collections.Generic;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface ISubscriberService
    {
        IDataResult<List<Subscriber>> GetAll();
        IDataResult<List<Subscriber>> GetByUserId(int id);
        IDataResult<List<Subscriber>> GetByChannelId(int id);
        IDataResult<Subscriber> GetById(int id);
        IResult Add(Subscriber subscriber);
        IResult Update(Subscriber subscriber);
        IResult Delete(int id);
    }
}
