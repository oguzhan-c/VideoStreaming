using System;
using Core.Entities;
using Core.Entities.Abstruct;

namespace Entities.Concrete
{
    public class VideoFile : IEntity
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string VideoPath { get; set; }
        public DateTime Date { get; set; }

    }
}
