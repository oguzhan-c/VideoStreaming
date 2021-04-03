using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface ILikeService
    {
        IDataResult<List<Like>> GetAll();
        IDataResult<Like> GetById(int id);
        IResult Add(Like like);
        IResult Update(Like like);
        IResult Delete(int id);
    }
}
