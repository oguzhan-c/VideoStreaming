using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class UserForRegisterDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        public string Gender { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime DateOfBorn { get; set; }
        public string RecoveryEmail { get; set; }
        public string ChannelName { get; set; }
    }
}