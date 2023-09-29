namespace Business.Models
{
    public class CreateProductRequestEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ProductImagePath { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsFeatured { get; set; }
        
    }
}
