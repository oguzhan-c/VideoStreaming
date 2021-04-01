using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface ICommunicationService
    {
        IDataResult<List<Communication>> GetAll();
        IDataResult<Communication> GetById(int id);
        IResult Add(Communication communication);
        IResult Delete(Communication communication);
        IResult Update(Communication communication);
    }
}
