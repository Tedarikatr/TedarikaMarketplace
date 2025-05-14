using Entity.Carriers;
using Entity.Orders;

namespace Services.Carriers.IServices
{
    public interface ICarrierWebhookService
    {
        Task SendOrderToCarrierAsync(Order order);
        Task<bool> NotifyShipmentCreatedAsync(Carrier carrier, string storeApiKey, object payload);
        Task<bool> HandleWebhookAsync(string carrierCode, string payload);
        Task<string> CreateShipmentAsync(int orderId);
        Task<string> TrackShipmentAsync(string trackingNumber);
        Task<bool> CancelShipmentAsync(string trackingNumber);
    }
}
