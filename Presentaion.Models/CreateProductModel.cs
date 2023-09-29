using Microsoft.AspNetCore.Http;

namespace Presentation.Models
{
    public class CreateProductModel
    {
        public bool IsFeatured { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile? Image { get; set; }
        public int DiscountPercent { get; set; } = 0;
        public List<int>? SelectedSubCategoryIds { get; set; }
    }
}
