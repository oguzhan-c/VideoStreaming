using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstruct;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Internal;

namespace Business.Concrete
{
    public class ChannelManager : IChannelService
    {
        private IChannelDal _channelDal;
        private IUserService _userService;
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
        }

        private IResult CheckIfChannelsExist()
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
        }

        private IResult CheckIfChannelExist(int id)
        {
            var result = _channelDal.GetAll(c => c.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ChannelMessages.ThisChannelsDoNotExist);
        }

        public IResult Add(Channel channel)
        {
            IResult result = BusinessRule.Run
            (
                ChackIfSameChannelNameExist(channel.ChannelName),
                _userService.CheckIfUserExist(channel.UserId)
            );

            if (result != null)
            {
                return result;
            }

            _channelDal.Add(channel);
            return new SuccessResult();
        }

        private IResult ChackIfSameChannelNameExist(string channelChannelName)
        {
            var result = _channelDal.GetAll(c => c.ChannelName == channelChannelName).Any();

            if (result)
            {
                return new ErrorResult(ChannelMessages.ThisChannelNameExist);
            }

            return new SuccessResult();
        }

        public IResult Update(Channel channel)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfChannelExist(channel.Id)
                );
            if (result != null)
            {
                return result;
            }

            _channelDal.Update(channel);
            return new SuccessResult();
        }

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
