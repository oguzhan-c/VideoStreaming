using Core.Entities;

namespace Entities.Concrete
{
    public class Communication : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }



    }
}
