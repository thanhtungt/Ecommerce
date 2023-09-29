namespace Business.Models
{
    public class CategoryResponseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; } = 0;
        public IEnumerable<CategoryResponseEntity>? SubCategory { get; set; }
    }
}
