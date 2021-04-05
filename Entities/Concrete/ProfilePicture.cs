using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class ProfilePicture : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PicturePath { get; set; }
        public DateTime Date { get; set; }


        public ProfilePicture()
        {
            Date = DateTime.Now;
        }
    }
}
