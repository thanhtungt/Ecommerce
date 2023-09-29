namespace Presentation.Models
{
    public class PagedResultModel<T> : PagedResultBaseModel
    {
        public T Items { set; get; }
    }
}
