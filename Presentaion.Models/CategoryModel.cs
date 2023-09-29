namespace Presentation.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; } = 0;
        public IEnumerable<CategoryModel>? SubCategory { get; set; }
    }
}
