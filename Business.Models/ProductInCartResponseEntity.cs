namespace Business.Models
{
    public class ProductInCartResponseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public string? ThumbnailImage { get; set; }
        public int Quantity { get; set; }
    }
}
