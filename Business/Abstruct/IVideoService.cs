using System.Collections.Generic;
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
        IResult Delete(int id);
        IResult CheckIfVideoExist(int id);
        IResult CheckIfVideosExist();
    }
}
