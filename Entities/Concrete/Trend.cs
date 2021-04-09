using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete
{
    public class Trend : IEntity
    {
        [Key]

        public int Id { get; set; }
        public int ChannelId { get; set; }
    }
}
