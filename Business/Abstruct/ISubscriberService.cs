using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface ISubscriberService
    {
        IDataResult<List<Subscriber>> GetAll();
        IDataResult<Subscriber> GetById(int id);
        IResult Add(Subscriber subscriber);
        IResult Update(Subscriber subscriber);
        IResult Delete(int id);
    }
}
