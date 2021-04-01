using System;
using Core.Entities;
using Core.Entities.Abstruct;

namespace Entities.Concrete
{
    public class Video : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public int TrendId { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public DateTime Duration { get; set; }
    }
}
