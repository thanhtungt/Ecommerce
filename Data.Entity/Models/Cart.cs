namespace Data.Entity.Models
{
    public class Cart
    {
        public int ProductId { set; get; }
        public Guid UserId { set; get; }
        public int Quantity { set; get; } = 1;
        public DateTime CreateAt { set; get; } = DateTime.Now;

        public Product Product { set; get; }
        public AppUser AppUser { set; get; }
    }
}
