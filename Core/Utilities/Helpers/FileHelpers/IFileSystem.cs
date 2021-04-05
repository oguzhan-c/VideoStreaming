using Core.Utilities.Results.Abstruct;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.FileHelpers
{
    public interface IFileSystem
    {
        string Add(IFormFile formFile, string path);
        IResult Delete(string sourcePath);
        string Update(IFormFile formFile, string oldSourcePath, string path);
    }
}
