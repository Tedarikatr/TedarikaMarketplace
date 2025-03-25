using MediatR;

namespace Domain.Products.Events
{
    public class ProductImageUpdatedEvent : INotification
    {
        public int ProductId { get; set; }
        public string NewImageUrl { get; set; }
    }
}
