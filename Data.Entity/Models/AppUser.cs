using Microsoft.AspNetCore.Identity;

namespace Data.Entity.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime Dob { set; get; }


        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
        public List<UserOrderInformation> UserOrderInformations { get; set; }
    }
}
