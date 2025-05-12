using MediatR;

namespace Domain.Stores.Events
{
    public class StoreCreatedEvent : INotification
    {
        public int StoreId { get; }
        public int SellerUserId { get; }

        public StoreCreatedEvent(int storeId, int sellerUserId)
        {
            StoreId = storeId;
            SellerUserId = sellerUserId;
        }
    }
}
