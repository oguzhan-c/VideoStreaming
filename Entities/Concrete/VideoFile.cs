using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete
{
    public class VideoFile : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string VideoPath { get; set; }
        public DateTime Date { get; set; }

    }
}
