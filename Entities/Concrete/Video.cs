using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete
{
    public class Video : IEntity
    {
        [Key]

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public int TrendId { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public int Duration { get; set; }
        public string VideoPath { get; set; }
        public string ThumbnailPath { get; set; }
        public DateTime Date { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
