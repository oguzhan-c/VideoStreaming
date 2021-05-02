using System;
using System.Collections.Generic;
using Business.Abstruct;
using Core.Utilities.BusinessRules;
using Core.Utilities.Helpers.FileHelpers.FileOnDiskManager;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.Helpers.FileHelpers;

namespace Business.Concrete
{
    public class ProfilePictureManager : IProfilePictureService
    {
        private readonly IProfilePictureDal _profilePictureDal;
        private readonly IUserService _userService;
        private readonly string _path = "ProfilePictures";
        private readonly IUserDetailService _userDetailService;
        private readonly IFileSystem _fileSystem;

        public ProfilePictureManager(IProfilePictureDal profilePictureDal, IUserService userService, IUserDetailService userDetailService, IFileSystem fileSystem)
        {
            _profilePictureDal = profilePictureDal;
            _userService = userService;
            _userDetailService = userDetailService;
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
            var selectedPhoto = _profilePictureDal.Get(pf => pf.Id == id);

            IResult result = BusinessRule.Run
                (
                    CheckIfProfilePhotoExist(id),
                    CheckIfProfilePhotoEmpty(selectedPhoto,_path)
                );
            if (result != null)
            {
                return new ErrorDataResult<ProfilePicture>(result.Message);
            }

            return new SuccessDataResult<ProfilePicture>(selectedPhoto);
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

        public IResult Add(IFormFile profilePhotoFile, ProfilePicture profilePicture)
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

            profilePicture.PicturePath = _fileSystem.Add(profilePhotoFile, _path);
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

        [SecuredOperation("User/Root")]
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

            _fileSystem.Delete(deleteToProfilePicture.PicturePath);

            _profilePictureDal.Delete(deleteToProfilePicture);

            return new SuccessResult();

        }

        [SecuredOperation("User/Root")]
        public IResult Update(IFormFile profilePhotoFile, ProfilePicture profilePicture)
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

            profilePicture.PicturePath = _fileSystem.Update(profilePhotoFile, profilePictureToUpdate.PicturePath, _path);
            profilePicture.Date = DateTime.Now;

            _profilePictureDal.Update(profilePicture);

            return new SuccessResult();
        }

        private IResult CheckIfProfilePhotoAlreadyDeleted(int id)
        {
            var result = _profilePictureDal.GetAll(pp => pp.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ProfilePhotoMessages.ThisProfilePhotoAlreadyDeleted);
        }


        IResult CheckIfProfilePhotoEmpty(ProfilePicture addedProfilePicture, string path)
        {
            var choseSex = _userDetailService.GetByUserId(addedProfilePicture.UserId).Data.Gender; 

            ProfilePicture profilePicture = new ProfilePicture();

            profilePicture.Id = addedProfilePicture.Id;
            profilePicture.Date = addedProfilePicture.Date;
            profilePicture.UserId = addedProfilePicture.UserId;

            if (choseSex.Equals("Male"))
            {
                profilePicture.PicturePath = $@"wwwroot\{path}\defaultMaleProfilePhoto";
            }
            profilePicture.PicturePath = $@"wwwroot\{path}\defaultFemaleProfilePhoto";

            return new SuccessResult();
        }
    }
}
