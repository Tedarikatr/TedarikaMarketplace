namespace Data.Dtos.Product
{
    public class ProductExportBannedDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string GTIPCode { get; set; }
        public string? CountryCode { get; set; }
        public bool IsExportBanned { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ProductExportBannedCreateDto
    {
        public int ProductId { get; set; }
        public string GTIPCode { get; set; }
        public List<string> CountryCodes { get; set; } = new();
        public bool IsExportBanned { get; set; } = true;
        public string Reason { get; set; }
    }

    public class ProductExportBannedUpdateDto
    {
        public int Id { get; set; }
        public bool IsExportBanned { get; set; }
        public string Reason { get; set; }
    }
}
