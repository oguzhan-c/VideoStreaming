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
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public IDataResult<List<Comment>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCommentsExist()
                );
            
            if (result != null)
            {
                return new ErrorDataResult<List<Comment>>(result.Message);
            }

            return new SuccessDataResult<List<Comment>>(_commentDal.GetAll());
        }

        private IResult CheckIfCommentsExist()
        {
            var result = _commentDal.GetAll().Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(CommentMessages.ThisCommentsDoNotExist);
        }

        public IDataResult<Comment> GetById(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCommentExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<Comment>(result.Message);
            }

            return new SuccessDataResult<Comment>(_commentDal.Get(c => c.Id == id));
        }

        [SecuredOperation("User/Root")]
        public IResult Add(Comment comment)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCommentAlreadyExist(comment.Id)
                );
            if (result != null)
            {
                return result;
            }

            _commentDal.Add(comment);

            return new SuccessResult();
        }

        private IResult CheckIfCommentAlreadyExist(int commentId)
        {
            var result = _commentDal.GetAll(c => c.Id == commentId).Any();

            if (result)
            {
                return new ErrorResult(CommentMessages.ThisCommentAlreadyExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Update(Comment comment)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCommentExist(comment.Id)
                );
            if (result != null)
            {
                return result;
            }

            _commentDal.Update(comment);

            return new SuccessResult();
        }

        private IResult CheckIfCommentExist(int commentId)
        {
            var result = _commentDal.GetAll(c => c.Id == commentId).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(CommentMessages.ThisCommentDoNotExist);
        }

        [SecuredOperation("User/Root")]
        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCommentAlreadyDeleted(id)

                );
            if (result != null)
            {
                return result;
            }

            var deleteToComment = _commentDal.Get(c => c.Id == id);
            _commentDal.Delete(deleteToComment);

            return new SuccessResult();
        }

        private IResult CheckIfCommentAlreadyDeleted(int id)
        {
            var result = _commentDal.GetAll(c => c.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(CommentMessages.ThisCommentAlreadyDeleted);
        }
    }
}
