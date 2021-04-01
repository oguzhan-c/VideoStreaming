using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface IVideoService
    {
        IDataResult<List<Video>> GetAll();
        IDataResult<Video> GetById(int id);
        IResult Add(Video video);
        IResult Update(Video video);
        IResult Delete(Video video);
    }
}
