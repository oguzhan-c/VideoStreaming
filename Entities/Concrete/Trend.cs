using Core.Entities;

namespace Entities.Concrete
{
    public class Trend : IEntity
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
    }
}
