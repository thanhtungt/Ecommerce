namespace Data.Entity.Models
{
    public class Product
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public int DiscountPercent { set; get; } = 0;
        public int Stock { set; get; }
        public DateTime CreateAt { set; get; } = DateTime.Now;
        public bool IsFeatured { set; get; } = false;
        public string? ProductImagePath { set; get; }

        public List<ProductInCategory> ProductInCategories { set; get; }
        public List<OrderDetail> OrderDetails { set; get; }
        public List<Cart> Carts { set; get; }
    }
}
