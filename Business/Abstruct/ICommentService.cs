using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface ICommentService
    {
        IDataResult<List<Comment>> GetAll();
        IDataResult<Comment> GetById(int id);
        IResult Add(Comment comment);
        IResult Update(Comment comment);
        IResult Delete(int id);
    }
}
