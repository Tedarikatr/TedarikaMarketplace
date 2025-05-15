namespace Entity.Products
{
    public class ProductExportBanned
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string GTIPCode { get; set; }

        public string? CountryCode { get; set; } 
        public bool IsExportBanned { get; set; } = true;
        public string Reason { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
