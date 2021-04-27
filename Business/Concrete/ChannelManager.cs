using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstruct;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.BusinessRules;
using Core.Utilities.Helpers.FileHelpers.FileOnDiskManager;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class ChannelManager : IChannelService
    {
        private readonly IChannelDal _channelDal;
        private readonly IUserService _userService;
        private string _path = "ChannelPhotos";

        public ChannelManager(IChannelDal channelDal, IUserService userService)
        {
            _channelDal = channelDal;
            _userService = userService;
        }

        public IDataResult<List<Channel>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfChannelsExist()
                );

            if (result != null)
            {
                return new ErrorDataResult<List<Channel>>(result.Message);
            }

            return new SuccessDataResult<List<Channel>>(_channelDal.GetAll());
        }

        public IResult CheckIfChannelsExist()
        {
            var result = _channelDal.GetAll().Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ChannelMessages.ThisChannelsDoNotExist);
        }

        public IDataResult<Channel> GetById(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfChannelExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<Channel>(result.Message);
            }

            return new SuccessDataResult<Channel>(_channelDal.Get(c => c.Id == id));
        }

        public IResult CheckIfChannelExist(int id)
        {
            var result = _channelDal.GetAll(c => c.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ChannelMessages.ThisChannelDoNotExist);
        }

        [SecuredOperation("User/Root")]
        public IDataResult<Channel> GetChannelPhoto(int id)
        {
            var result = BusinessRule.Run
                (
                    CheckIfChannelExist(id)
                );

            if (result != null)
            {
                return new ErrorDataResult<Channel>(result.Message);
            }

            return new SuccessDataResult<Channel>(_channelDal.Get(c => c.Id == id).ChannelPhotoPath);
        }
        [SecuredOperation("User/Root")]
        public IResult Add(Channel channel)
        {
            var result = BusinessRule.Run
            (
                CheckIfChannelAlreadyExist(channel.Id), 
                CheckIfSameChannelNameExist(channel.ChannelName),
                _userService.CheckIfUserExist(channel.UserId)
            );

            if (result != null)
            {
                return result;
            }

            channel.InstallationDate = DateTime.Now;

            _channelDal.Add(channel);
            
            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult AddChannelPhoto(IFormFile channelPhotoFile , int id)
        {
            var result = BusinessRule.Run
                (
                    CheckIfChannelExist(id)
                );

            if (result != null)
            {
                return result;
            }

            var selectedChannel = _channelDal.Get(c => c.Id == id);

            selectedChannel.ChannelPhotoPath = FileOnDiskManager.Add(channelPhotoFile, _path);

            _channelDal.Add(selectedChannel);

            return new SuccessResult();
        }

        private IResult CheckIfChannelAlreadyExist(int channelId)
        {
            var result = _channelDal.GetAll(c => c.Id == channelId).Any();

            if (result)
            {
                return new ErrorResult(ChannelMessages.ThisChannelAlreadyExist);
            }

            return new SuccessResult();
        }

        private IResult CheckIfSameChannelNameExist(string channelName)
        {
            var result = _channelDal.GetAll(c => c.ChannelName == channelName).Any();

            if (result)
            {
                return new ErrorResult(ChannelMessages.ThisChannelNameExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Update(IFormFile channelPhotoFile,Channel channel)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfChannelExist(channel.Id)
                );
            if (result != null)
            {
                return result;
            }

            var channelToUpdate = _channelDal.Get(cp => cp.Id == channel.Id);

            FileOnDiskManager.Update(channelPhotoFile, channelToUpdate.ChannelPhotoPath, _path);
            channel.InstallationDate = channelToUpdate.InstallationDate;
            channel.UpdateDate = DateTime.Now;

            _channelDal.Update(channel);

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Delete(int id)
        {
            var deleteToChannel = _channelDal.Get(c => c.Id == id);

            IResult result = BusinessRule.Run
                (
                    CheckIfChannelAlreadyDeleted(deleteToChannel.ChannelName)
                );

            if (result != null)
            {
                return result;
            }

            FileOnDiskManager.Delete(_channelDal.Get(c => c.Id == id).ChannelPhotoPath);

            _channelDal.Delete(deleteToChannel);
            
            return new SuccessResult();
        }


        private IResult CheckIfChannelAlreadyDeleted(string channelName)
        {
            var result = _channelDal.GetAll(c => c.ChannelName == channelName).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ChannelMessages.ThisChannelAlreadyDeleted);
        }
    }
}
