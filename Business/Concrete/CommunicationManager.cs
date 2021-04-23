using System.Collections.Generic;
using Business.Abstruct;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results.Abstruct;
using Core.Utilities.Results.Concrute;
using DataAccess.Abstruct;
using Entities.Concrete;
using System.Linq;
using Business.Constant;

namespace Business.Concrete
{
    public class CommunicationManager : ICommunicationService
    {
        private readonly ICommunicationDal _communicationDal;

        public CommunicationManager(ICommunicationDal communicationDal)
        {
            _communicationDal = communicationDal;
        }

        public IDataResult<List<Communication>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCommunicationsExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<Communication>>(result.Message);
            }

            return new SuccessDataResult<List<Communication>>(_communicationDal.GetAll());
        }

        private IResult CheckIfCommunicationsExist()
        {
            var result = _communicationDal.GetAll().Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(CommunicationMessages.ThisCommunicationsDoNotExist);
        }

        public IDataResult<Communication> GetById(int id)
        {
            IResult result = BusinessRule.Run
                (   
                    CheckIfCommunicationExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<Communication>(result.Message);
            }

            return new SuccessDataResult<Communication>(_communicationDal.Get(c => c.Id == id));
        }

        private IResult CheckIfCommunicationExist(int id)
        {
            var result = _communicationDal.GetAll(c => c.Id == id).Any();

            if (result)
            {
                return new  SuccessResult();
            }

            return new ErrorResult(CommunicationMessages.ThisCommunicationDoNotExist);
        }

        public IResult Add(Communication communication)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCommunicationAlreadyExist(communication.Id)
                );
            if (result != null)
            {
                return result;
            }

            _communicationDal.Add(communication);
            return new SuccessResult();
        }

        private IResult CheckIfCommunicationAlreadyExist(int communicationId)
        {
            var result = _communicationDal.GetAll(c => c.Id == communicationId).Any();

            if (result)
            {
                return new ErrorResult(CommunicationMessages.ThisCommunicationAlreadyExist);
            }

            return new SuccessResult();
        }


        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCommunicationAlreadyDeleted(id)
                );
            if (result != null)
            {
                return result;
            }

            var deleteToCommunication = _communicationDal.Get(c => c.Id == id);
            _communicationDal.Delete(deleteToCommunication);
            return new SuccessResult();
        }

        private IResult CheckIfCommunicationAlreadyDeleted(int communicationId)
        {
            var result = _communicationDal.GetAll(c => c.Id == communicationId).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(CommunicationMessages.ThisCommunicationAlreadyDeleted);
        }

        public IResult Update(Communication communication)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfCommunicationExist(communication.Id)
                );
            if (result != null)
            {
                return result;
            }

            _communicationDal.Update(communication);
            return new SuccessResult();
        }
    }
}
