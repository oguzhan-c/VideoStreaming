
using System;
using System.Collections.Generic;
using Business.Abstruct;
using Business.Constant;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;
using System.Linq;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Helpers.FileHelpers;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    class UserDetailManager : IUserDetailService
    {
        private readonly IUserDetailDal _userDetailDal;
        private readonly IUserService _userService;
        private string _path = "ProfilePhotos";
        private IFileSystem _fileSystem;

        public UserDetailManager(IUserDetailDal userDetailDal, IUserService userService, IFileSystem fileSystem)
        {
            _userDetailDal = userDetailDal;
            _userService = userService;
            _fileSystem = fileSystem;
        } 
        public IDataResult<List<UserDetail>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailsExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<UserDetail>>(result.Message);
            }

            return new SuccessDataResult<List<UserDetail>>(_userDetailDal.GetAll());
        }

        private IResult CheckIfUserDetailsExist()
        {
            var result = _userDetailDal.GetAll().Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(UserDetailMessages.UsersDoNotExist);
        }

        public IResult Add(UserDetail userDetail)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailAlreadyExist(userDetail.Id)
                );
            if (result != null)
            {
                return result;
            }

            userDetail.PhotoPath = "DefaultPhoto";

            _userDetailDal.Add(userDetail);
            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult AddProfilePhoto(IFormFile photoFile, int id)
        {
            var selectedPhoto = _userDetailDal.Get(ud => ud.Id == id);

            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUserExist(selectedPhoto.UserId),
                    CheckIfProfilePhotoAlreadyExist(selectedPhoto.Id)
                );

            if (result != null)
            {
                return result;
            }

            selectedPhoto.PhotoPath = _fileSystem.Add(photoFile, _path);

            _userDetailDal.Add(selectedPhoto);

            return new SuccessResult();
        }

        private IResult CheckIfProfilePhotoAlreadyExist(int selectedPhotoId)
        {
            var result = _userDetailDal.Get(ud => ud.Id == selectedPhotoId).PhotoPath.Any();

            if (result)
            {
                return new ErrorResult(UserDetailMessages.ThisProfilePhotoAlreadyExist);
            }

            return new SuccessResult();
        }


        private IResult CheckIfUserDetailAlreadyExist(int userDetailId)
        {
            var result = _userDetailDal.GetAll(ud => ud.Id == userDetailId).Any();

            if (result)
            {
                return new ErrorResult(UserDetailMessages.UserDetailAlreadyExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailAlreadyDeleted(id)
                );
            if (result != null)
            {
                return result;
            }

            var deleteToUserDetail = _userDetailDal.Get(ud => ud.Id == id);
            _userDetailDal.Delete(deleteToUserDetail);
            return new SuccessResult();
        }

        private IResult CheckIfUserDetailAlreadyDeleted(int userDetailId)
        {
            var result = _userDetailDal.GetAll(ud => ud.Id == userDetailId).Any();

            if (result)
            {
                return new ErrorResult(UserDetailMessages.UserDetailAlreadyDeleted);
            }

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Update(UserDetail userDetail)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailExist(userDetail.Id)
                );
            if (result != null)
            {
                return result;
            }

            _userDetailDal.Update(userDetail);
            return new SuccessResult();
        }

        private IResult CheckIfUserDetailExist(int userDetailId)
        {
            var result = _userDetailDal.GetAll(ud => ud.Id == userDetailId).Any();
            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(UserDetailMessages.UserDetailDoNotExist);
        }

        public IDataResult<UserDetail> GetById(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfUserDetailExist(id),
                    CheckIfProfilePhotoEmpty(_userDetailDal.Get(ud => ud.Id == id),_path)
                );
            if (result != null)
            {
                return new ErrorDataResult<UserDetail>(result.Message);
            }

            return new SuccessDataResult<UserDetail>(CheckIfProfilePhotoEmpty(_userDetailDal.Get(ud => ud.Id == id), _path).Data);
        }

        public IDataResult<UserDetail> GetByUserId(int userId)
        {
            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUserExist(userId)
                );

            if (result != null)
            {
                return new ErrorDataResult<UserDetail>(UserMessages.UserDoNotExist);
            }

            return new SuccessDataResult<UserDetail>(_userDetailDal.Get(ud => ud.UserId == userId));
        }


        IDataResult<UserDetail> CheckIfProfilePhotoEmpty(UserDetail addedUserDetail, string path)
        {
            if (!addedUserDetail.PhotoPath.Equals("DefaultPhoto"))
            {
                return new SuccessDataResult<UserDetail>(_userDetailDal.Get(ud => ud.Id == addedUserDetail.Id));
            }

            var choseSex = addedUserDetail.Gender;

            UserDetail userDetail = new UserDetail();

            userDetail.Id = addedUserDetail.Id;
            userDetail.Gender = addedUserDetail.Gender;
            userDetail.DateOfJoin = addedUserDetail.DateOfJoin;
            userDetail.UserId = addedUserDetail.UserId;
            userDetail.RecoveryEmail = addedUserDetail.RecoveryEmail;
            userDetail.DateOfBorn = addedUserDetail.DateOfBorn;
            userDetail.IdentityNumber = addedUserDetail.IdentityNumber;

            if (choseSex.Equals("Male"))
            {
                userDetail.PhotoPath = $@"wwwroot\{path}\defaultMaleProfilePhoto";
            }
            userDetail.PhotoPath = $@"wwwroot\{path}\defaultFemaleProfilePhoto";

            return new SuccessDataResult<UserDetail>(userDetail);
        }
    }

    
}
