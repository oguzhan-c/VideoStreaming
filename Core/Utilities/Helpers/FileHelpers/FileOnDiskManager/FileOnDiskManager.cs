using System;
using System.IO;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.FileHelpers.FileOnDiskManager
{
    public class FileOnDiskManager : IFileSystem
    {
        public String Add(IFormFile formFile,string path)
        {
            var result = NewFilePath(formFile,path);
            try
            {
                var sourcePath = Path.GetTempFileName();
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(sourcePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                    File.Move(sourcePath, result);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public IResult Delete(string sourcePath)
        {
            try
            {
                File.Delete(sourcePath);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public string Update(IFormFile formFile, String oldSourcePath , string path)
        {
            try
            {
                var result = NewFilePath(formFile, path);
                if (oldSourcePath.Length > 0)
                {
                    using (var stream = new FileStream(result,FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                }
                File.Delete(oldSourcePath);
                return result;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }

        private static string NewFilePath(IFormFile formFile,string sourcePath)
        {


            var rootPath = Environment.CurrentDirectory + @"\wwwroot\" + sourcePath;

            var fileExtension = Path.GetExtension(rootPath + formFile.FileName);
            var newGuidPath = Guid.NewGuid().ToString() + "_" +
                              DateTime.Now.Day + "_" +
                              DateTime.Now.Month + "_" +
                              DateTime.Now.Year + "_" +
                              fileExtension;
            var result = $@"{sourcePath}\{newGuidPath}";
            return result;
        }
    }
}