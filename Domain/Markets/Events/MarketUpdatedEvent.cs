using MediatR;

namespace Domain.Markets.Events
{
    public class MarketUpdatedEvent : INotification
    {
        public int MarketId { get; set; }
        public string Name { get; set; }
        public string RegionCode { get; set; }

        public MarketUpdatedEvent(int marketId, string name, string regionCode)
        {
            MarketId = marketId;
            Name = name;
            RegionCode = regionCode;
        }
    }
}
