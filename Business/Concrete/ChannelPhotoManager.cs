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
    public class ChannelPhotoManager : IChannelPhotoService
    {
        private readonly IChannelPhotoDal _channelPhotoDal;
        private readonly IChannelService _channelService;
        private readonly string _path = "ChannelPhotos";

        public ChannelPhotoManager(IChannelPhotoDal channelPhotoDal, IChannelService channelService)
        {
            _channelPhotoDal = channelPhotoDal;
            _channelService = channelService;
        }

        public IDataResult<List<ChannelPhoto>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfChannelPhotosExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<ChannelPhoto>>(result.Message);
            }

            return new SuccessDataResult<List<ChannelPhoto>>(_channelPhotoDal.GetAll());
        }

        private IResult CheckIfChannelPhotosExist()
        {
            var result = _channelPhotoDal.GetAll().Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ChannelPhotoMessages.ThisPhotosDoNotExist);
        }

        public IDataResult<ChannelPhoto> GetById(int id)
        {
            var selectedChannelPhoto = _channelPhotoDal.Get(cp => cp.Id == id);

            IResult result = BusinessRule.Run
                (
                    _channelService.CheckIfChannelExist(selectedChannelPhoto.ChannelId),
                    CheckIfChannelPhotoExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<ChannelPhoto>(result.Message);
            }

            return new SuccessDataResult<ChannelPhoto>(_channelPhotoDal.Get(cp => cp.Id == id));
        }

        public IDataResult<List<ChannelPhoto>> GetChannelPhotosByChannelId(int id)
        {
            IResult result = BusinessRule.Run
                (
                    _channelService.CheckIfChannelExist(id),
                    CheckIfChannelPhotoEmpty(id)
                );

            if (result != null)
            {
                return new ErrorDataResult<List<ChannelPhoto>>(result.Message);
            }

            return new SuccessDataResult<List<ChannelPhoto>>(_channelPhotoDal.GetAll(cp => cp.ChannelId == id));
        }

        private IDataResult<List<ChannelPhoto>> CheckIfChannelPhotoEmpty(int id)
        {
            string rootPath = $@"{Environment.CurrentDirectory}\wwwroot\" + _path + @"\logo.jpg";

            List<ChannelPhoto> defaultChannelPhotos = new List<ChannelPhoto>();

            ChannelPhoto channelPhoto = new ChannelPhoto {ChannelId = id,ImagePath = rootPath, Date = DateTime.Now};

            defaultChannelPhotos.Add(channelPhoto);
            return new ErrorDataResult<List<ChannelPhoto>>(defaultChannelPhotos,ChannelPhotoMessages.AddedDefaultPhoto);
        }

        public IResult Add(IFormFile file, ChannelPhoto channelPhoto)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfChannelPhotoAlreadyExist(channelPhoto.Id),
                    _channelService.CheckIfChannelExist(channelPhoto.ChannelId)
                );

            if (result != null)
            {
                return result;
            }

            channelPhoto.ImagePath = new FileOnDiskManager().Add(file, _path);
            channelPhoto.Date = DateTime.Now;
            _channelPhotoDal.Add(channelPhoto);
            return new SuccessResult();
        }

        public IResult Update(IFormFile file, ChannelPhoto channelPhoto)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfChannelPhotoExist(channelPhoto.Id)
                );
            if (result != null)
            {
                return result;
            }

            var channelPhotoToUpdate = _channelPhotoDal.Get(cp => cp.Id == channelPhoto.Id);
            channelPhoto.ChannelId = channelPhotoToUpdate.ChannelId;
            channelPhoto.ImagePath = new FileOnDiskManager().Update(file, channelPhotoToUpdate.ImagePath, _path);
            channelPhoto.Date = DateTime.Now;
            _channelPhotoDal.Add(channelPhoto);

            return new SuccessResult();
        }

        private IResult CheckIfChannelPhotoExist(int channelPhotoId)
        {
            var result = _channelPhotoDal.GetAll(cp => cp.Id == channelPhotoId).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ChannelPhotoMessages.ThisPhotoDoNotExist);
        }

        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfChannelPhotoAlreadyDeleted(id)
                );
            if (result != null)
            {
                return result;
            }

            var deleteToChannelPhoto = _channelPhotoDal.Get(cp => cp.Id == id);
            new FileOnDiskManager().Delete(deleteToChannelPhoto.ImagePath);
            _channelPhotoDal.Delete(deleteToChannelPhoto);

            return new SuccessResult();
        }

        private IResult CheckIfChannelPhotoAlreadyDeleted(int id)
        {
            var result = _channelPhotoDal.GetAll(cp => cp.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ChannelPhotoMessages.ThisPhotoAlreadyDeleted);
        }

        private IResult CheckIfChannelPhotoAlreadyExist(int channelPhotoId)
        {
            var result = _channelPhotoDal.GetAll(cp => cp.Id == channelPhotoId).Any();
            if (result)
            {
                return new ErrorResult(ChannelPhotoMessages.ThisPhotoAlreadyExist);
            }
            return new SuccessResult();

        }
    }
}
