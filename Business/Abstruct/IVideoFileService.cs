using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface IVideoFileService
    {
        IDataResult<List<VideoFile>> GetAll();
        IDataResult<VideoFile> GetById(int id);
        IDataResult<VideoFile> GetByVideoId(int videoId);
        IResult Add(VideoFile videoFile);
        IResult Delete(VideoFile videoFile);
        IResult Update(VideoFile videoFile);
    }
}
