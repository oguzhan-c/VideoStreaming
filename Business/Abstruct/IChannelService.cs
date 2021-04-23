using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstruct
{
    public interface  IChannelService
    {
        IDataResult<List<Channel>> GetAll();
        IDataResult<Channel> GetById(int id);
        IDataResult<Channel> GetChannelPhoto(int id);
        IResult Add(Channel channel);
        IResult AddChannelPhoto(IFormFile channelPhotoFile, int id);
        IResult Update(IFormFile channelPhotoFile, Channel channel);
        IResult Delete(int id);
        IResult CheckIfChannelsExist();
        IResult CheckIfChannelExist(int id);
    }
}
