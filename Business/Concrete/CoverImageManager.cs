using System;
using System.Collections.Generic;
using Business.Abstruct;
using Business.Constat;
using Core.Utilities.BusinessRules;
using Core.Utilities.Helpers.FileHelpers.FileOnDiskManager;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace Business.Concrete
{
    public class CoverImageManager : ICoverImageService
    {
        private readonly ICoverImageDal _coverImageDal;
        private readonly IVideoService _videoService;
        private readonly IUserService _userService;
        private const string Path = "CoverImages";

        public CoverImageManager(ICoverImageDal coverImageDal, IVideoService videoService, IUserService userService)
        {
            _coverImageDal = coverImageDal;
            _videoService = videoService;
            _userService = userService;
        }

        public IDataResult<List<CoverImage>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUsersExist(),
                    _videoService.CheckIfVideosExist(),
                    CheckIfCoverImagesExist()
                );

            if (result != null)
            {
                return new ErrorDataResult<List<CoverImage>>(result.Message);
            }

            return new SuccessDataResult<List<CoverImage>>(_coverImageDal.GetAll());

        }

        private IResult CheckIfCoverImagesExist()
        {
            var result = _coverImageDal.GetAll().Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(CoverImageMessages.ThisVideoCoverImagesDoNotExist);
        }

        public IDataResult<CoverImage> GetById(int id)
        {
            var selectedCoverImage = _coverImageDal.Get(ci => ci.Id == id);

            IResult result = BusinessRule.Run
                (
                    CheckIfCoverImageExist(id),
                    _userService.CheckIfUserExist(selectedCoverImage.UserId),
                    _videoService.CheckIfVideoExist(selectedCoverImage.VideoId)
                );

            if (result != null)
            {
                return new ErrorDataResult<CoverImage>(result.Message);
            }

            return new SuccessDataResult<CoverImage>(selectedCoverImage);
        }
        private IResult CheckIfCoverImageExist(int id)
        {
            var result = _coverImageDal.GetAll(ci => ci.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(CoverImageMessages.ThisVideoCoverImageDoNotExist);

        }

        public IResult Add(IFormFile file, CoverImage coverImage)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCoverImageAlreadyExist(coverImage.Id)
                );

            if (result != null)
            {
                return result;
            }

            coverImage.ImagePath = new FileOnDiskManager().Add(file, Path);
            coverImage.Date = DateTime.Now;
            _coverImageDal.Add(coverImage);

            return new SuccessResult();
        }

        private IResult CheckIfCoverImageAlreadyExist(int coverImageId)
        {
            var result = _coverImageDal.GetAll(ci => ci.Id == coverImageId).Any();

            if (result)
            {
                return new ErrorResult(CoverImageMessages.ThisVideoCoverImageAlreadyExist);
            }

            return new SuccessResult();
        }

        public IResult Update(IFormFile file, CoverImage coverImage)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCoverImageExist(coverImage.Id)
                );

            if (result != null)
            {
                return result;
            }

            var coverImageToUpdate = _coverImageDal.Get(ci => ci.Id == coverImage.Id);
            coverImage.UserId = coverImageToUpdate.UserId;
            coverImage.VideoId = coverImageToUpdate.VideoId;
            coverImage.ImagePath = new FileOnDiskManager().Update(file, coverImageToUpdate.ImagePath, Path);
            coverImage.Date = DateTime.Now;

            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCoverImageAlreadyDeleted(id)
                );

            if (result != null)
            {
                return result;

            }

            var deleteToCoverImage = _coverImageDal.Get(ci => ci.Id == id);
            new FileOnDiskManager().Delete(deleteToCoverImage.ImagePath);
            _coverImageDal.Delete(deleteToCoverImage);

            return new SuccessResult();
        }

        private IResult CheckIfCoverImageAlreadyDeleted(int id)
        {
            var result = _coverImageDal.GetAll(ci => ci.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(CoverImageMessages.ThisVideoCoverImageAlreadyDeleted);
        }


        public IDataResult<List<CoverImage>> GetByUserId(int id)
        {
            IResult result = BusinessRule.Run
            (
                _userService.CheckIfUserExist(id),
                CheckIfCoverImagesExist()
            );
            if (result != null)
            {
                return new ErrorDataResult<List<CoverImage>>(result.Message);
            }

            return new SuccessDataResult<List<CoverImage>>(_coverImageDal.GetAll(ci => ci.UserId == id));

        }


        public IDataResult<List<CoverImage>> GetByVideoId(int id)
        {
            IResult result = BusinessRule.Run
                (
                    _videoService.CheckIfVideoExist(id),
                    CheckIfCoverImagesExist()
                );

            if (result != null)
            {
                return new ErrorDataResult<List<CoverImage>>(result.Message);
            }

            return new SuccessDataResult<List<CoverImage>>(_coverImageDal.GetAll(ci => ci.VideoId == id));
        }
    }
}
