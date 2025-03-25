using Entity.Companies;
using Entity.Orders;

namespace Entity.Stores.Payments
{
    public class StoreInvoice
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }

        public string InvoiceNumber { get; set; }
    }
}
