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
    public class SubscriberManager : ISubscriberService
    {
        private readonly ISubscriberDal _subscriberDal;
        private readonly IChannelService _channelService;
        private readonly IUserService _userService;

        public SubscriberManager(ISubscriberDal subscriberDal, IChannelService channelService, IUserService userService)
        {
            _subscriberDal = subscriberDal;
            _channelService = channelService;
            _userService = userService;
        }

        [SecuredOperation("/UserRoot")]
        public IDataResult<List<Subscriber>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfSubscribersExist()
                );
            if (result != null)
            {
                return new ErrorDataResult<List<Subscriber>>(result.Message);
            }

            return new SuccessDataResult<List<Subscriber>>(_subscriberDal.GetAll());
        }
        [SecuredOperation("User/Root")]
        public IDataResult<List<Subscriber>> GetByUserId(int id)
        {
            IResult result = BusinessRule.Run
                (
                    _userService.CheckIfUserExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<List<Subscriber>>(result.Message);
            }

            return new SuccessDataResult<List<Subscriber>>(_subscriberDal.GetAll(s => s.UserId == id));
        }
        [SecuredOperation("User/Root")]
        public IDataResult<List<Subscriber>> GetByChannelId(int id)
        {
            IResult result = BusinessRule.Run
                (
                    _channelService.CheckIfChannelExist(id)
                );
            if (result != null)
            {
                return new ErrorDataResult<List<Subscriber>>(result.Message);
            }

            return new SuccessDataResult<List<Subscriber>>(_subscriberDal.GetAll(s => s.ChannelId == id));
        }

        private IResult CheckIfSubscribersExist()
        {
            var result = _subscriberDal.GetAll().Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(SubscriberMessages.ThisSubscribersDoNotExist);
        }

        [SecuredOperation("User/Root")]
        public IDataResult<Subscriber> GetById(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfSubscriberExist(id)
                );
            if (result!= null)
            {
                return new ErrorDataResult<Subscriber>(result.Message);
            }

            return new SuccessDataResult<Subscriber>(_subscriberDal.Get(s => s.Id == id));
        }

        private IResult CheckIfSubscriberExist(int id)
        {
            var result = _subscriberDal.GetAll(s => s.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(SubscriberMessages.ThisSubscriberDoNotExist);
        }

        [SecuredOperation("User/Root")]
        public IResult Add(Subscriber subscriber)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfSubscriberAlreadyExist(subscriber.Id)
                );

            if (result != null)
            {
                return result;
            }

            _subscriberDal.Add(subscriber);
            return new SuccessResult();
        }

        private IResult CheckIfSubscriberAlreadyExist(int subscriberId)
        {
            var result = _subscriberDal.GetAll(s => s.Id == subscriberId).Any();

            if (result)
            {
                return new ErrorResult(SubscriberMessages.ThisSubscriberAlreadyExist);
            }

            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Update(Subscriber subscriber)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfSubscriberExist(subscriber.Id)
                );
            if (result != null)
            {
                return result;
            }

            _subscriberDal.Update(subscriber);
            return new SuccessResult();
        }

        [SecuredOperation("User/Root")]
        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfSubscriberAlreadyDeleted(id)
                );
            
            if (result != null)
            {
                return result;
            }
            
            var deleteToSubscriber = _subscriberDal.Get(s => s.Id == id);
            _subscriberDal.Delete(deleteToSubscriber);

            return new SuccessResult();
        }

        private IResult CheckIfSubscriberAlreadyDeleted(int id)
        {
            var result = _subscriberDal.GetAll(s => s.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(SubscriberMessages.ThisSubscriberAlreadyDeleted);
        }
    }
}
