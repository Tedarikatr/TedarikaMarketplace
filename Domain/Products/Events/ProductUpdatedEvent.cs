using MediatR;

namespace Domain.Products.Events
{
    public class ProductUpdatedEvent : INotification
    {
        public int ProductId { get; set; }
        public string NewName { get; set; }
        public string NewBrand { get; set; }
        public string NewImageUrl { get; set; }
        public string NewDescription { get; set; }
        public string NewUnitTypes { get; set; }
    }
}
