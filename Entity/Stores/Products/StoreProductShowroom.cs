namespace Entity.Stores.Products
{
    public class StoreProductShowroom
    {
        public int Id { get; set; }

        public int StoreProductId { get; set; }
        public StoreProduct StoreProduct { get; set; }

        public string MediaType { get; set; } 
        public string MediaUrl { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int DisplayOrder { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
