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
    public class VideoFileManager : IVideoFileService
    {
        private readonly IVideoFileDal _videoFileDal;
        private readonly IVideoService _videoService;
        private readonly string _path = @"Uploads\Videos";

        public VideoFileManager(IVideoFileDal videoFileDal, IVideoService videoService)
        {
            _videoFileDal = videoFileDal;
            _videoService = videoService;
        }

        public IDataResult<List<VideoFile>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    _videoService.CheckIfVideosExist(),
                    CheckIfVideoFilesExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<VideoFile>>(result.Message);
            }

            return new SuccessDataResult<List<VideoFile>>(_videoFileDal.GetAll());
        }

        private IResult CheckIfVideoFilesExist()
        {
            var result = _videoFileDal.GetAll().Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(VideoFileMessages.ThisVideoFilesDoNotExist);
        }

        public IDataResult<VideoFile> GetById(int id)
        {
            IResult result = BusinessRule.Run
            (
                _videoService.CheckIfVideoExist(_videoFileDal.Get(vf=>vf.Id == id).VideoId),
                CheckIfVideoFileExist(id)
            );

            if (result != null)
            {
                return new ErrorDataResult<VideoFile>(result.Message);
            }

            return new SuccessDataResult<VideoFile>(_videoFileDal.Get(vf => vf.Id == id));
        }

        public IDataResult<VideoFile> GetByVideoId(int videoId)
        {
            IResult result = BusinessRule.Run
            (
                CheckIfVideoFileExist(_videoFileDal.Get(vf=>vf.VideoId == videoId).Id),
                _videoService.CheckIfVideoExist(videoId)
            );

            if (result != null)
            {
                return new ErrorDataResult<VideoFile>(result.Message);
            }

            return new SuccessDataResult<VideoFile>(_videoFileDal.Get(vf => vf.VideoId == videoId));
        }

        public IResult Add(IFormFile file,VideoFile videoFile)
        {
            IResult result = BusinessRule.Run
            (
                CheckIfVideoFileAlreadyExist(videoFile.Id),
                _videoService.CheckIfVideoExist(videoFile.VideoId)
            );

            if (result != null)
            {
                return result;
            }

            videoFile.VideoPath = new FileOnDiskManager().Add(file, _path);
            videoFile.Date = DateTime.Now;
            _videoFileDal.Add(videoFile);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfVideoFileAlreadyDeleted(id)
                );
            if (result != null)
            {
                return result;
            }

            var deleteToVideoFile = _videoFileDal.Get(vf => vf.Id == id);
            new FileOnDiskManager().Delete(deleteToVideoFile.VideoPath);
            _videoFileDal.Delete(deleteToVideoFile);

            return new SuccessResult();
        }

        private IResult CheckIfVideoFileAlreadyDeleted(int id)
        {
            var result = _videoFileDal.GetAll(vf => vf.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(VideoFileMessages.ThisVideoFileAlreadyDeleted);
        }

        private IResult CheckIfVideoFileAlreadyExist(int videoFileId)
        {
            var result = _videoFileDal.GetAll(vf => vf.Id == videoFileId).Any();

            if (result)
            {
                return new ErrorResult(VideoFileMessages.ThisVideoFileAlreadyExist);
            }

            return new SuccessResult();
        }

        public IResult Update(IFormFile file, VideoFile videoFile)
        {
            IResult result = BusinessRule.Run
            (
                CheckIfVideoFileExist(videoFile.Id)
            );
            if (result != null)
            {
                return result;
            }

            var videoFileToUpdate = _videoFileDal.Get(vf => vf.Id == videoFile.Id);
            videoFile.VideoId = videoFileToUpdate.VideoId;
            videoFile.VideoPath = new FileOnDiskManager().Update(file, videoFileToUpdate.VideoPath, _path);
            videoFile.Date = DateTime.Now;

            return new SuccessResult();
        }

        private IResult CheckIfVideoFileExist(int videoFileId)
        {
            var result = _videoFileDal.GetAll(vf => vf.Id == videoFileId).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(VideoFileMessages.ThisVideoFileDoNotExist);
        }
    }
}
