using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface ITrendService
    {
        IDataResult<List<Trend>> GetAll();
        IResult Add(Trend trend);
        IResult Update(Trend trend);
        IResult Delete(Trend trend);
    }
}
