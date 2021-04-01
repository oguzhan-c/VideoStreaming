using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface ICoverImageService
    {
        IDataResult<List<CoverImage>> GetAll();
        IDataResult<CoverImage> GetById(int id);
        IResult Add(CoverImage coverImage);
        IResult Update(CoverImage coverImage);
        IResult Delete(CoverImage coverImage);
    }
}
