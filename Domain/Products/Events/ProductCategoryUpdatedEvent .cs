using MediatR;

namespace Domain.Categories.Events
{
    public class ProductCategorySubUpdatedEvent : INotification
    {
        public int CategorySubId { get; set; }
        public string NewSubCategoryName { get; set; }
    }
}
