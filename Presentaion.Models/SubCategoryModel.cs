namespace Presentation.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public bool Selected { get; set; }
    }
}
