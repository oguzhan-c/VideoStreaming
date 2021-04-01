﻿using System;
using Core.Entities;
using Core.Entities.Abstruct;

namespace Entities.Concrete
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public int PostedByUserId { get; set; }
        public int ResponseByUserId { get; set; }
        public int LikeId { get; set; }
        public int DislikeId { get; set; }
        public string CommentBody { get; set; }
        public DateTime Date { get; set; }


    }
}
