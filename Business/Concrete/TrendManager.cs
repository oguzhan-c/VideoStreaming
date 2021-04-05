using System.Collections.Generic;
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
    public class TrendManager : ITrendService
    {
        private readonly ITrendDal _trendDal;
        private readonly IChannelService _channelService;

        public TrendManager(ITrendDal trendDal, IChannelService channelService)
        {
            this._trendDal = trendDal;
            _channelService = channelService;
        }


        public IDataResult<List<Trend>> GetAll()
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfTrendsExist(),
                    _channelService.CheckIfChannelsExist()
                );

            if (result != null)
            {
                return new ErrorDataResult<List<Trend>>(result.Message);
            }

            return new SuccessDataResult<List<Trend>>(_trendDal.GetAll());

        }

        public IDataResult<Trend> GetById(int id)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfTrendExist(id),
                    _channelService.CheckIfChannelExist(_trendDal.Get(t=>t.Id == id).ChannelId)
                );
            if (result != null)
            {
                return new ErrorDataResult<Trend>(result.Message);
            }

            return new SuccessDataResult<Trend>(_trendDal.Get(t => t.Id == id));
        }

        public IDataResult<List<Trend>> GetByChannelId(int channelId)
        {
            IResult result = BusinessRule.Run
                (
                    _channelService.CheckIfChannelExist(channelId)
                );

            if (result != null)
            {
                return new ErrorDataResult<List<Trend>>(result.Message);
            }

            return new SuccessDataResult<List<Trend>>(_trendDal.GetAll(t => t.ChannelId == channelId));
        }

        private IResult CheckIfTrendsExist()
        {
            var result = _trendDal.GetAll().Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(TrendMessages.ThisTrendsDoNotExist);
        }
        public IResult Add(Trend trend)
        {
            IResult result = BusinessRule.Run
                (
                    CheckIfTrendAlreadyExist(trend.Id),
                    _channelService.CheckIfChannelExist(trend.ChannelId)
                );
            if (result != null)
            {
                return result;
            }
            _trendDal.Add(trend);
            return new SuccessResult();
        }

        private IResult CheckIfTrendAlreadyExist(int trendId)
        {
            var result = _trendDal.GetAll(t => t.Id == trendId).Any();

            if (result)
            {
                return new ErrorResult(TrendMessages.ThisTrendAlreadyExist);
            }

            return new SuccessResult();
        }

        public IResult Update(Trend trend)
        {
            IResult result = BusinessRule.Run
                (
                    _channelService.CheckIfChannelExist(trend.ChannelId),
                    CheckIfTrendExist(trend.Id)
                );
            
            if (result != null)
            {
                return result;
            }

            _trendDal.Update(trend);
            return new SuccessResult();
        }

        private IResult CheckIfTrendExist(int trendId)
        {
            var result = _trendDal.GetAll(t => t.Id == trendId).Any();

            if (result)
            {
                return new ErrorResult(TrendMessages.ThisTrendDoNotExist);
            }

            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            IResult result = BusinessRule.Run
                (
                CheckIfTrendExist(id),
                    CheckIfTrendAlreadyDeleted(id)
                );

            if (result != null)
            {
                return result;
            }

            var deleteToTrend = _trendDal.Get(t => t.Id == id);
            _trendDal.Delete(deleteToTrend);

            return new SuccessResult();

        }

        private IResult CheckIfTrendAlreadyDeleted(int id)
        {
            var result = _trendDal.GetAll(t => t.Id == id).Any();

            if (result)
            {
                return new SuccessResult();
            }

            return new ErrorResult(TrendMessages.ThisTrendAlreadyDeleted);
        }
    }
}
