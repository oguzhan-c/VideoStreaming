using System.Collections.Generic;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface ITrendService
    {
        IDataResult<List<Trend>> GetAll();
        IDataResult<Trend> GetById(int id);
        IDataResult<List<Trend>> GetByChannelId(int channelId);
        IResult Add(Trend trend);
        IResult Update(Trend trend);
        IResult Delete(int id);
    }
}
