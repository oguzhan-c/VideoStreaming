using System.Collections.Generic;
using Business.Abstruct;
using Business.Constat;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Internal;

namespace Business.Concrete
{
    public class VideoManager : IVideoService
    {
        private readonly IVideoDal _videoDal;
        private readonly IUserService _userService;
        private readonly IChannelService _channelService;


        public VideoManager(IVideoDal videoDal, IChannelService channelService, IUserService userService)
        {
            _videoDal = videoDal;
            _channelService = channelService;
            _userService = userService;
        }

        public IDataResult<List<Video>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    _channelService.CheckIfChannelsExist(),
                    _userService.CheckIfUsersExist(),
                    CheckIfVideosExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<Video>>(result.Message);
            }

            return new SuccessDataResult<List<Video>>(_videoDal.GetAll());
        }

        public IResult CheckIfVideosExist()
        {
            var result = _videoDal.GetAll().Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(VideoMessages.ThisVideosDoNotExist);
        }

        public IDataResult<Video> GetById(int id)
        {
            var getToVideo = _videoDal.Get(v => v.Id == id);
            IResult result = BusinessRule.Run
                (
                    CheckIfVideoExist(id),
                    _userService.CheckIfUserExist(getToVideo.UserId),
                    _channelService.CheckIfChannelExist(getToVideo.ChannelId)
            );

            if (result != null)
            {
                return new ErrorDataResult<Video>(result.Message);
            }

            return new SuccessDataResult<Video>(getToVideo);
        }

        public IResult CheckIfVideoExist(int id)
        {
            var result = _videoDal.GetAll(v=>v.Id == id).Any();
            if (result)
            {
                return new ErrorResult(VideoMessages.ThisVideoDoNotExist);
            }

            return new SuccessResult();

        }

        public IResult Add(Video video)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfVideoAlreadyExist(video.Id),
                   _userService.CheckIfUserExist(video.UserId),
                    _channelService.CheckIfChannelExist(video.ChannelId)
            );

            if (result != null)
            {
                return result;
            }

            _videoDal.Add(video);
            return new SuccessResult();
        }

        private IResult CheckIfVideoAlreadyExist(int videoId)
        {
            var result = _videoDal.GetAll(v=>v.Id == videoId).Any();

            if (result)
            {
                return new ErrorResult(VideoMessages.ThisVideoAlreadyExist);
            }

            return new SuccessResult();
        }

        public IResult Update(Video video)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfVideoExist(video.Id)
                );

            if (result != null)
            {
                return result;
            }

            _videoDal.Update(video);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfVideoAlreadyDeleted(id)
                );
            if (result != null)
            {
                return result;
            }

            var deleteToVideo = _videoDal.Get(v => v.Id == id);
            _videoDal.Delete(deleteToVideo);
            return new SuccessResult();
        }

        private IResult CheckIfVideoAlreadyDeleted(int id)
        {
            var result = _videoDal.GetAll(v => v.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(VideoMessages.ThisVideoAlreadyDeleted);
        }
    }
}
