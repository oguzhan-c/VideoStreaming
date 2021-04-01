﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface IChannelPhotoService
    {
        IDataResult<List<ChannelPhoto>> GetAll();
        IDataResult<ChannelPhoto> GetById(int id);
        IResult Add(ChannelPhoto channelPhoto);
        IResult Update(ChannelPhoto channelPhoto);
        IResult Delete(ChannelPhoto channelPhoto);
    }
}
