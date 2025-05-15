using MediatR;

namespace Domain.Products.Events
{
    public class ProductExportBanLiftedEvent : INotification
    {
        public int ProductId { get; set; }
        public string GTIPCode { get; set; }
        public string? CountryCode { get; set; }
    }
}
