using System;
using Core.Entities;
using Core.Entities.Abstruct;

namespace Entities.Concrete
{
    public class Subscriber : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public DateTime Date { get; set; }
    }
}
