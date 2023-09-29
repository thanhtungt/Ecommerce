namespace Presentation.Models
{
    public class AddToCartModel
    {
        public Guid UserId { get; set; }
        public int ProductId { get; set;}
        public int Quantity { get; set;}
    }
}
