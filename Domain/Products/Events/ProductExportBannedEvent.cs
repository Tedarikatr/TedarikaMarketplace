using MediatR;

namespace Domain.Products.Events
{
    public class ProductExportBannedEvent : INotification
    {
        public int ProductId { get; set; }
        public string GTIPCode { get; set; }
        public string? CountryCode { get; set; }
        public string Reason { get; set; }
    }
}
