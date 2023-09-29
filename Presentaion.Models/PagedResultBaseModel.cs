namespace Presentation.Models
{
    public class PagedResultBaseModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { set; get; }
        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
