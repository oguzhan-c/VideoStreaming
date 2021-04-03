using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class UserDetail : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime DateOfBorth { get; set; }
        public string RecoveryEmail { get; set; }
        public DateTime DateOfJoin { get; set; }
    }
}
