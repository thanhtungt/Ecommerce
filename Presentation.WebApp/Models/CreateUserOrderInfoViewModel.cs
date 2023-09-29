namespace Presentation.WebApp.Models
{
    public class CreateUserOrderInfoViewModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
        public AddressType AddressType { get; set; }
    }

    public enum AddressType
    {
        HOME,OFFICE
    }
}
