using System;
using System.Collections.Generic;
using System.Text;
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
    public class ProfilePictureManager : IProfilePictureService
    {
        private IProfilePictureDal _profilePictureDal;

        public ProfilePictureManager(IProfilePictureDal profilePictureDal)
        {
            _profilePictureDal = profilePictureDal;
        }

        public IDataResult<List<ProfilePicture>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfProfilePhotosExsist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<ProfilePicture>>(result.Message);
            }

            return new SuccessDataResult<List<ProfilePicture>>(_profilePictureDal.GetAll());
        }

        private IResult CheckIfProfilePhotosExsist()
        {
            var result = _profilePictureDal.GetAll().Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ProfilePhotoMessages.ThisProfilePhotosDoNotExist);
        }

        public IDataResult<Entities.Concrete.ProfilePicture> GetById(int id)
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

        private IResult CheckIfProfilePhotoExist(int id)
        {
            var result = _profilePictureDal.GetAll(pf => pf.Id == id).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(ProfilePhotoMessages.ThisProfilePhotoDoNotExist);
        }

        public IResult Add(ProfilePicture profilePicture)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfProfilePhotoAlreadyExist(profilePicture.Id)
                );
            if (result != null)
            {
                return result;
            }
            _profilePictureDal.Add(profilePicture);
            return new SuccessResult();
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
            var deleteToProfilePicture = _profilePictureDal.Get(pf => pf.Id == id);
            _profilePictureDal.Delete(deleteToProfilePicture);

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


        public IResult Update(ProfilePicture profilePicture)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfProfilePhotoExist(profilePicture.Id)
                );
            if (result != null)
            {
                return result;
            }

            _profilePictureDal.Update(profilePicture);
            return new SuccessResult();
        }
    }
}
