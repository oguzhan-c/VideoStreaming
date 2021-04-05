using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface IChannelService
    {
        IDataResult<List<Channel>> GetAll();
        IDataResult<Channel> GetById(int id);
        IResult Add(Channel channel);
        IResult Update(Channel channel);
        IResult Delete(int id);
        IResult CheckIfChannelExist(int id);
        IResult CheckIfChannelsExist();
    }
}
