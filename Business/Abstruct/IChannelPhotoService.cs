
using System.Collections.Generic;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstruct
{
    public interface IChannelPhotoService
    {
        IDataResult<List<ChannelPhoto>> GetAll();
        IDataResult<ChannelPhoto> GetById(int id);
        IResult Add(IFormFile file, ChannelPhoto channelPhoto);
        IResult Update(IFormFile file, ChannelPhoto channelPhoto);
        IDataResult<List<ChannelPhoto>> GetChannelPhotosByChannelId(int id);
        IResult Delete(int id);
    }
}
