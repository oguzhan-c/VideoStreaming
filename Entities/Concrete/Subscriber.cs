using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
namespace Entities.Concrete
{
    public class Subscriber : IEntity
    {
        [Key]

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public DateTime Date { get; set; }
    }
}
