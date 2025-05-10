using MediatR;

namespace Domain.Stores.Events
{
    public class StoreLocationCoverageSyncRequestedEvent : INotification
    {
        public int StoreId { get; set; }

        public StoreLocationCoverageSyncRequestedEvent(int storeId)
        {
            StoreId = storeId;
        }
    }
}
