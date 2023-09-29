using Presentation.Models;

namespace Presentation.AdminApp.Models
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsFeatured { get; set; }
        public IFormFile? Image { get; set; }
        public IEnumerable<SubCategoryModel>? SubCategories { get; set; }
        public List<int> SelectedSubCategoryIds { get; set; }
    }
}
