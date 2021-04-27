using System.Collections.Generic;
using System.Linq;
using Business.Abstruct;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;

namespace Business.Concrete
{
    public class LikeManager : ILikeService
    {
        private readonly ILikeDal _likeDal;
        private readonly IUserService _userService;
        private readonly IVideoService _videoService;

        public LikeManager(ILikeDal likeDal, IUserService userService, IVideoService videoService)
        {
            _likeDal = likeDal;
            _userService = userService;
            _videoService = videoService;
        }

        public IDataResult<List<Like>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUsersExist(),
                    _videoService.CheckIfVideosExist(),
                    CheckIfLikesExist()
                );

            if (result != null)
            {
                return new ErrorDataResult<List<Like>>(result.Message);
            }

            return new SuccessDataResult<List<Like>>(_likeDal.GetAll());
        }

        private IResult CheckIfLikesExist()
        {
            var result = _likeDal.GetAll().Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(LikeMessages.ThisLikesDoNotExist);
        }

        public IDataResult<Like> GetById(int id)
        {
            var selectedLike = _likeDal.Get(l => l.Id == id);

            IResult result = BusinessRule.Run
                (
                    CheckIfLikeExist(id),
                    _userService.CheckIfUserExist(selectedLike.UserId),
                    _videoService.CheckIfVideoExist(selectedLike.VideoId)
                );


            if (result != null)
            {
                return new ErrorDataResult<Like>(result.Message);
            }

            return new SuccessDataResult<Like>(selectedLike);
        }

        private IResult CheckIfLikeExist(int id)
        {
            var result = _likeDal.GetAll(l => l.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(LikeMessages.ThisLikeDoNotExist);
        }

        [SecuredOperation("User/Root")]
        public IResult Add(Like like)
        {
            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUserExist(like.UserId),
                    _videoService.CheckIfVideoExist(like.VideoId),
                    CheckIfLikeAlreadyExist(like.Id)
            );

            if (result != null)
            {
                return result;
            }

            _likeDal.Add(like);
            return new SuccessResult();
        }

        private IResult CheckIfLikeAlreadyExist(int likeId)
        {
            var result = _likeDal.GetAll(l => l.Id == likeId).Any();

            if (result)
            {
                return new ErrorResult(LikeMessages.ThisLikeAlreadyExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Update(Like like)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfLikeExist(like.Id)
                );

            if (result != null)
            {
                return result;
            }

            _likeDal.Update(like);
            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfLikeAlreadyDeleted(id)
                );
            if (result != null)
            {
                return result;
            }

            var deleteToLike = _likeDal.Get(l => l.Id == id);
            _likeDal.Delete(deleteToLike);

            return new SuccessResult();
        }

        private IResult CheckIfLikeAlreadyDeleted(int id)
        {
            var result = _likeDal.GetAll(l => l.Id == id).Any();

            if(result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(LikeMessages.ThisLikeAlreadyDeleted);
        }
    }
}
