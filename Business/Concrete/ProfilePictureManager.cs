﻿using System;
using System.Collections.Generic;
using Business.Abstruct;
using Business.Constat;
using Core.Utilities.BusinessRules;
using Core.Utilities.Helpers.FileHelpers;
using Core.Utilities.Helpers.FileHelpers.FileOnDiskManager;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace Business.Concrete
{
    public class ProfilePictureManager : IProfilePictureService
    {
        private readonly IProfilePictureDal _profilePictureDal;
        private readonly IUserService _userService;
        private IFileSystem _fileSystem;
        private readonly string _path = "ProfilePictures";

        public ProfilePictureManager(IProfilePictureDal profilePictureDal, IUserService userService, IFileSystem fileSystem)
        {
            _profilePictureDal = profilePictureDal;
            _userService = userService;
            _fileSystem = fileSystem;
        }

        public IDataResult<List<ProfilePicture>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfProfilePhotosExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<ProfilePicture>>(result.Message);
            }

            return new SuccessDataResult<List<ProfilePicture>>(_profilePictureDal.GetAll());
        }

        private IResult CheckIfProfilePhotosExist()
        {
            var result = _profilePictureDal.GetAll().Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ProfilePhotoMessages.ThisProfilePhotosDoNotExist);
        }

        public IDataResult<ProfilePicture> GetById(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfProfilePhotoExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<ProfilePicture>(result.Message);
            }

            return new SuccessDataResult<ProfilePicture>(_profilePictureDal.Get(pf => pf.Id == id));
        }

        public IDataResult<List<ProfilePicture>> GetByUserId(int id)
        {
            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUserExist(id)
                );

            if (result != null)
            {
                return new ErrorDataResult<List<ProfilePicture>>(result.Message);
            }

            return new SuccessDataResult<List<ProfilePicture>>(_profilePictureDal.GetAll(pp=>pp.UserId == id));
        }

        public IResult Add(IFormFile file, ProfilePicture profilePicture)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfProfilePhotoAlreadyExist(profilePicture.Id),
                    _userService.CheckIfUserExist(profilePicture.UserId)
                );
            if (result!= null)
            {
                return result;
            }

            profilePicture.PicturePath = _fileSystem.Add(file, _path);
            profilePicture.Date = DateTime.Now;                                                                                                                                     
            _profilePictureDal.Add(profilePicture);

            return new SuccessResult();
        }

        private IResult CheckIfProfilePhotoExist(int id)
        {
            var result = _profilePictureDal.GetAll(pf => pf.Id == id).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ProfilePhotoMessages.ThisProfilePhotoDoNotExist);
        }

        private IResult CheckIfProfilePhotoAlreadyExist(int profilePictureId)
        {
            var result = _profilePictureDal.GetAll(pf => pf.Id == profilePictureId).Any();
            if (result)
            {
                return new ErrorResult(ProfilePhotoMessages.ThisProfilePhotoAlreadyExist);
            }

            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                CheckIfProfilePhotoAlreadyDeleted(id)
                );
            if (result != null)
            {
                return result;
            }

            var deleteToProfilePicture = _profilePictureDal.Get(pf => pf.Id == id);
            new FileOnDiskManager().Delete(deleteToProfilePicture.PicturePath);
            _profilePictureDal.Delete(deleteToProfilePicture);

            return new SuccessResult();

        }

        public IResult Update(IFormFile file, ProfilePicture profilePicture)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfProfilePhotoExist(profilePicture.Id),
                    _userService.CheckIfUserExist(profilePicture.UserId)
                );
            if (result != null)
            {
                return result;
            }

            var profilePictureToUpdate = _profilePictureDal.Get(pp => pp.Id == profilePicture.Id);

            profilePicture.Id = profilePictureToUpdate.Id;
            profilePicture.PicturePath =
                new FileOnDiskManager().Update(file, profilePictureToUpdate.PicturePath, _path);
            profilePicture.Date = DateTime.Now;

            return new SuccessResult();
        }

        private IResult CheckIfProfilePhotoAlreadyDeleted(int id)
        {
            var result = _profilePictureDal.GetAll(pf => pf.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ProfilePhotoMessages.ThisProfilePhotoAlreadyDeleted);
        }

    }
}
