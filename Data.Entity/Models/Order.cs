using Data.Entity.Enums;

namespace Data.Entity.Models
{
    public class Order
    {
        public int Id { set; get; }
        public DateTime CreateAt { set; get; }
        public Guid UserId { set; get; }
        public OrderStatus OrderStatus { set; get; }
        public int UserOrderInfoId { set; get; }

        public List<OrderDetail> OrderDetails { set; get; }
        public AppUser AppUser { set; get; }
        public UserOrderInformation UserOrderInformation { set; get; }
    }
}
