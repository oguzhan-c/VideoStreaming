﻿using Core.Entities;

namespace Entities.Concrete
{
    public class Like : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }

    }
}
