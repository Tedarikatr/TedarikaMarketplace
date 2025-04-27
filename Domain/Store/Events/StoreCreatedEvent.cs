using MediatR;

namespace Domain.Store.Events
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
