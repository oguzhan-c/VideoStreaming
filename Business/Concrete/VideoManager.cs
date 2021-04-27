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
    public class VideoManager : IVideoService
    {
        private readonly IVideoDal _videoDal;
        private readonly IUserService _userService;
        private readonly IChannelService _channelService;
        private string _videoPath = "Videos";
        private string _thumbnailPath = "Thumbnails";



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
            var result = _videoDal.GetAll(v => v.Id == id).Any();
            if (result)
            {
                return new SuccessResult();

            }
            return new ErrorResult(VideoMessages.ThisVideoDoNotExist);


        }

        public IDataResult<Video> GetVideoFile(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfVideoExist(id)
                );

            if (result != null)
            {
                return new ErrorDataResult<Video>(result.Message);
            }

            var selectedVideo = _videoDal.Get(v => v.Id == id);

            return new SuccessDataResult<Video>(_videoDal.Get(v => v.VideoPath == selectedVideo.VideoPath));
        }

        public IDataResult<Video> GetThumbnail(int id)
        {

            IResult result = BusinessRule.Run
            (
                CheckIfVideoExist(id)
            );

            if (result != null)
            {
                return new ErrorDataResult<Video>(result.Message);
            }

            var selectedVideo = _videoDal.Get(v => v.Id == id);

            return new SuccessDataResult<Video>(_videoDal.Get(v => v.ThumbnailPath == selectedVideo.ThumbnailPath));
        }

        [SecuredOperation("User/Root")]
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
            
            video.Date = DateTime.Now;

            _videoDal.Add(video);
            
            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult AddVideoFile(IFormFile videoFile, int id)
        {
            var result = BusinessRule.Run
                (
                    CheckIfVideoExist(id)
                );

            if (result != null)
            {
                return result;
            }

            var selectedVideo = _videoDal.Get(v => v.Id == id);

            selectedVideo.VideoPath = FileOnDiskManager.Add(videoFile, _videoPath);
            selectedVideo.Date = DateTime.Now;

            _videoDal.Add(selectedVideo);

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult AddVideoThumbnail(IFormFile thumbnailFile, int id)
        {
            var result = BusinessRule.Run
                (
                    CheckIfVideoExist(id)
                );

            if (result != null)
            {
                return result;
            }

            var selectedVideo = _videoDal.Get(v => v.Id == id);

            selectedVideo.VideoPath = FileOnDiskManager.Add(thumbnailFile, _thumbnailPath);
            selectedVideo.Date = DateTime.Now;

            _videoDal.Add(selectedVideo);

            return new SuccessResult();
        }


        private IResult CheckIfVideoAlreadyExist(int videoId)
        {
            var result = _videoDal.GetAll(v => v.Id == videoId).Any();

            if (result)
            {
                return new ErrorResult(VideoMessages.ThisVideoAlreadyExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Update(IFormFile videoFile, IFormFile thumbnailFile, Video video)
        {
            var result = BusinessRule.Run
                (
                    CheckIfVideoExist(video.Id)
                );

            if (result != null)
            {
                return result;
            }

            var videoToUpdate = _videoDal.Get(v => v.Id == video.Id);

            video.VideoPath = FileOnDiskManager.Update(videoFile, videoToUpdate.VideoPath, _videoPath);
            video.ThumbnailPath = FileOnDiskManager.Update(thumbnailFile, videoToUpdate.ThumbnailPath, _thumbnailPath);
            video.Date = videoToUpdate.Date;
            videoToUpdate.UpdateDate = DateTime.Now;

            _videoDal.Update(video);

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
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

            FileOnDiskManager.Delete(deleteToVideo.VideoPath);
            FileOnDiskManager.Delete(deleteToVideo.ThumbnailPath);

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
