using MediatR;

namespace Domain.Products.Events
{
    public class ProductCategoryUpdatedEvent : INotification
    {
        public int CategoryId { get; set; }
        public string NewCategoryName { get; set; }
    }
}
