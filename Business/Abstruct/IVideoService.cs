using System.Collections.Generic;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstruct
{
    public interface IVideoService
    {
        IDataResult<List<Video>> GetAll();
        IDataResult<Video> GetById(int id);
        IDataResult<Video> GetThumbnail(int id);
        IDataResult<Video> GetVideoFile(int id);
        IResult Add(Video video);
        IResult AddVideoFile(IFormFile videoFile, int id);
        IResult AddVideoThumbnail(IFormFile thumbnailFile, int id);
        IResult Update(IFormFile videoFile, IFormFile thumbnailFile, Video video);
        IResult Delete(int id);
        IResult CheckIfVideoExist(int id);
        IResult CheckIfVideosExist();
    }
}
