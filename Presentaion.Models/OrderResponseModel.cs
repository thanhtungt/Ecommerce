

namespace Presentation.Models
{
    public class OrderResponseModel
    {
        public UserOrderInformationModel UserOrderInfo { get; set; }
        public IEnumerable<OrderProductResponseModel> Products { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreateAt { get; set; }
        public int OrderId { get; set; }
    }
}
