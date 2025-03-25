using MediatR;

namespace Domain.Products.Events
{
    public class StoreProductRequestApprovedEvent : INotification
    {
        public int RequestId { get; set; }
        public int StoreId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string UnitTypes { get; set; }
        public int UnitType { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int? CategorySubId { get; set; }
        public string CategorySubName { get; set; }

        public string ImageUrl { get; set; }
        public DateTime? PreparationTime { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
