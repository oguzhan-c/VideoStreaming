﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results.Abstruct;
using Entities.Concrete;

namespace Business.Abstruct
{
    public interface IProfilePictureService
    {
        IDataResult<List<ProfilePicture>> GetAll();
        IDataResult<ProfilePicture> GetById(int id);
        IResult Add(ProfilePicture profilePicture);
        IResult Delete(int id);
        IResult Update(ProfilePicture profilePicture);
    }
}
