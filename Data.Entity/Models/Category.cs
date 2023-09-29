namespace Data.Entity.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int ParentId { get; set; } = 0;

        public Category? Parent { get; set; }
        public List<Category>? SubCategory { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
    }
}
