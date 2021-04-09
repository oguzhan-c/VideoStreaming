using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete
{
    public class Channel : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ChannelName { get; set; }
        public DateTime InstallationDate { get; set; }
        public string Description { get; set; }
    }
}
