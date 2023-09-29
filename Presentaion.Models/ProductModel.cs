namespace Presentation.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsFeatured { get; set; }
        public string? ProductImagePath { get; set; }
        public int Stock { get; set; }
    }
}
