using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class CoverImage : IEntity
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
    }
}
