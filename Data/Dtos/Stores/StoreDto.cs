namespace Data.Dtos.Stores
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string ImageUrl { get; set; }
        public string StorePhone { get; set; }
        public string StoreMail { get; set; }
        public string StoreProvince { get; set; }
        public string StoreDistrict { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
    }

    public class StoreCreateDto
    {
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string ImageUrl { get; set; }
        public string StorePhone { get; set; }
        public string StoreMail { get; set; }
        public string StoreProvince { get; set; }
        public string StoreDistrict { get; set; }
    }

    public class StoreUpdateDto
    {
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string ImageUrl { get; set; }
        public string StorePhone { get; set; }
        public string StoreMail { get; set; }
        public string StoreProvince { get; set; }
        public string StoreDistrict { get; set; }
    }

    public class StoreStatusDto
    {
        public int StoreId { get; set; }
        public bool IsActive { get; set; }
    }
}
