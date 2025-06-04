using MediatR;

namespace Domain.Stores.Events
{
    public class StoreCoverageChangedEvent : INotification
    {
        public int StoreId { get; }

        public StoreCoverageChangedEvent(int storeId)
        {
            StoreId = storeId;
        }
    }
}
