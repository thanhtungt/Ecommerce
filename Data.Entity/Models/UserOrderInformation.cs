using Data.Entity.Enums;

namespace Data.Entity.Models
{
    public class UserOrderInformation
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
        public AddressType AddressType { get; set; }

        public AppUser AppUser { get; set; }
        public List<Order> Orders { get; set; }
    }
}
