using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Entity.Baskets
{
    public class Basket
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }

        public string Currency { get; set; } = "TRY";
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.All)]
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
