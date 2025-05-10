using MediatR;

namespace Domain.Stores.Events
{
    public class StoreLocationCoverageChangedEvent : INotification
    {
        public int StoreId { get; set; }

        public StoreLocationCoverageChangedEvent(int storeId)
        {
            StoreId = storeId;
        }
    }
}
