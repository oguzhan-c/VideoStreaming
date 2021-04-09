using System.Collections.Generic;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstruct
{
    public interface IProfilePictureService
    {
        IDataResult<List<ProfilePicture>> GetAll();
        IDataResult<ProfilePicture> GetById(int id);
        IDataResult<List<ProfilePicture>> GetByUserId(int id);
        IResult Add(IFormFile file, ProfilePicture profilePicture);
        IResult Delete(int id);
        IResult Update(IFormFile file, ProfilePicture profilePicture);
        //IDataResult<List<ProfilePicture>> GetByUserId(int userId); yarın yap
    }
}
