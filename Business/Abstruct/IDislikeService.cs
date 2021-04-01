using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface IDislikeService
    {
        IDataResult<List<Dislike>> GetAll();
        IDataResult<Dislike> GetById(int id);
        IResult Add(Dislike dislike);
        IResult Update(Dislike dislike);
        IResult Delete(Dislike dislike);
    }
}
