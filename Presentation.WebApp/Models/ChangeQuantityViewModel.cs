namespace Presentation.WebApp.Models
{
    public class ChangeQuantityViewModel
    {
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
