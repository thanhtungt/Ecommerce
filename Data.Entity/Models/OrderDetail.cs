namespace Data.Entity.Models
{
    public class OrderDetail
    {
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public Order Order { set; get; }
        public Product Product { set; get; }    
    }
}
