using System.Collections.Generic;
using System.Linq;
using Business.Abstruct;
using Business.Constant;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;

namespace Business.Concrete
{
     public class DislikeManager : IDislikeService
     {
         private readonly IDislikeDal _dislikeDal;

         public DislikeManager(IDislikeDal dislikeDal)
         {
             _dislikeDal = dislikeDal;
         }

         public IDataResult<List<Dislike>> GetAll()
         {
             IResult result = BusinessRule.Run
                 (
                    CheckIfDislikesExist()
                 );

             if (result != null)
             {
                 return new ErrorDataResult<List<Dislike>>(result.Message);
             }

             return new SuccessDataResult<List<Dislike>>(_dislikeDal.GetAll());
         }

         private IResult CheckIfDislikesExist()
         {
             var result = _dislikeDal.GetAll().Any();

             if (result)
             {
                 return new SuccessResult();
             }

             return new ErrorResult(DislikeMessages.ThisDislikesDoNotExist);
         }

         public IDataResult<Dislike> GetById(int id)
         {
             IResult result = BusinessRule.Run
                 (
                    CheckIfDislikeExist(id)
                 );
             if (result != null)
             {
                 return new ErrorDataResult<Dislike>(result.Message);
             }

             return new SuccessDataResult<Dislike>(_dislikeDal.Get(d => d.Id == id));
         }

         private IResult CheckIfDislikeExist(int id)
         {
             var result = _dislikeDal.GetAll(d => d.Id == id).Any();

             if (result)
             {
                 return new SuccessResult();
             }

             return new ErrorResult(DislikeMessages.ThisDislikeDoNotExist);
         }

         public IResult Add(Dislike dislike)
         {

             IResult result = BusinessRule.Run
                 (
                    CheckIfDislikeAlreadyExist(dislike.Id)
                 );
             if (result != null)
             {
                 return result;
             }

             _dislikeDal.Add(dislike);
             return new SuccessResult();
         }

         private IResult CheckIfDislikeAlreadyExist(int dislikeId)
         {
             var result = _dislikeDal.GetAll(d => d.Id == dislikeId).Any();

             if (result)
             {
                 return new ErrorResult(DislikeMessages.ThisDilakeAlreadyExist);
             }

             return new SuccessResult();
         }

         public IResult Update(Dislike dislike)
         {

             IResult result = BusinessRule.Run
                 (
                    CheckIfDislikeExist(dislike.Id)
                 );

             if (result != null)
             {
                 return result;
             }

             _dislikeDal.Update(dislike);
             return new SuccessResult();
         }

         public IResult Delete(int id)
         {

             IResult result = BusinessRule.Run
                 (
                    CheckIfDislikeAlreadyDeleted(id)
                 );

             if (result != null)
             {
                 return result;
             }

             var deleteToDislike = _dislikeDal.Get(d => d.Id == id);
             _dislikeDal.Delete(deleteToDislike);
             return new SuccessResult();
         }

         private IResult CheckIfDislikeAlreadyDeleted(int id)
         {
             var result = _dislikeDal.GetAll(d => d.Id == id).Any();

             if (result)
             {
                 return new SuccessResult();
             }

             return new ErrorResult(DislikeMessages.ThisDislikeAlreadyDeleted);
         }
    }
}
