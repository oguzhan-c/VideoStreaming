using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete
{
    public class ProfilePicture : IEntity
    {
        [Key]

        public int Id { get; set; }
        public int UserId { get; set; }
        public string PicturePath { get; set; }
        public DateTime Date { get; set; }
    }
}
