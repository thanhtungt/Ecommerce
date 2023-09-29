using Data.Entity.Enums;

namespace Business.Models
{
    public class OrderResponseEntity
    {
        public UserOrderInformationEntity UserOrderInfo { get; set; }
        public IEnumerable<OrderProductResponseEntity> Products { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreateAt { get; set; }
        public int OrderId { get; set; }
    }
}
