using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstruct
{
    public interface IVideoFileService
    {
        IDataResult<List<VideoFile>> GetAll();
        IDataResult<VideoFile> GetById(int id);
        IDataResult<VideoFile> GetByVideoId(int videoId);
        IResult Add(IFormFile file,VideoFile videoFile);
        IResult Delete(int id);
        IResult Update(IFormFile file,VideoFile videoFile);
    }
}
