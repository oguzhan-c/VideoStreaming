using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete
{
    public class Dislike : IEntity
    {
        [Key]

        public int Id { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
    }
}
