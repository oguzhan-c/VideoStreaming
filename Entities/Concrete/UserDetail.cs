using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete
{
    public class UserDetail : IEntity
    {
        [Key]

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime DateOfBorn { get; set; }
        public string RecoveryEmail { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string PhotoPath { get; set; }
    }
}
