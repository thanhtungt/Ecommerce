namespace Business.Models
{
    public class PagedResultEntity<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { set; get; }

        public T Items { set; get; }
    }
}
