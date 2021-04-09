using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstruct
{
    public interface ICoverImageService
    {
        IDataResult<List<CoverImage>> GetAll();
        IDataResult<CoverImage> GetById(int id);
        IDataResult<List<CoverImage>> GetByUserId(int id);
        IDataResult<List<CoverImage>> GetByVideoId(int id);
        IResult Add(IFormFile file, CoverImage coverImage);
        IResult Update(IFormFile file, CoverImage coverImage);
        IResult Delete(int id);
    }
}
