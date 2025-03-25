using MediatR;

namespace Domain.Products.Events
{
    public class ProductCategorySubUpdatedEvent : INotification
    {
        public int CategorySubId { get; set; }
        public string NewSubCategoryName { get; set; }
    }
}
