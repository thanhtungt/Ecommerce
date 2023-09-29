namespace Business.Models
{
    public class SubCategoryResponseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; } = false;
        public string ParentName { get; set; }
    }
}
