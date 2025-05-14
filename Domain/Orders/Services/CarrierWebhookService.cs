using Entity.Carriers;
using Entity.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Carriers.IRepositorys;
using Services.Carriers.IServices;
using System.Net.Http.Json;
using System.Text;

namespace Services.Carriers.Services
{
    public class CarrierWebhookService : ICarrierWebhookService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICarrierRepository _carrierRepository;
        private readonly ILogger<CarrierWebhookService> _logger;

        public CarrierWebhookService(ILogger<CarrierWebhookService> logger)
        {
            _logger = logger;
        }

        public async Task SendOrderToCarrierAsync(Order order)
        {
            try
            {
                var carrier = await _carrierRepository.GetQueryable()
                    .FirstOrDefaultAsync(c => c.Id == order.SelectedCarrierId);

                if (carrier == null)
                {
                    _logger.LogWarning("OrderId {OrderId} için taşıyıcı bulunamadı.", order.Id);
                    return;
                }

                var payload = new
                {
                    orderId = order.Id,
                    orderNumber = order.OrderNumber,
                    storeId = order.StoreId,
                    totalAmount = order.TotalAmount,
                    createdAt = order.CreatedAt,
                    deliveryAddressId = order.DeliveryAddressId
                    // Genişletilebilir: ürünler, alıcı bilgileri vs.
                };

                var client = _httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("OrderId {OrderId} için {CarrierName} webhook gönderiliyor...", order.Id, carrier.Name);

                var response = await client.PostAsync(carrier.ApiEndpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Carrier webhook başarısız oldu. OrderId: {OrderId}, StatusCode: {StatusCode}", order.Id, response.StatusCode);
                    // TODO: Retry mekanizması ya da loglama stratejisi
                }
                else
                {
                    _logger.LogInformation("Carrier webhook başarıyla gönderildi. OrderId: {OrderId}", order.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OrderId {OrderId} için taşıyıcı webhook gönderiminde hata oluştu.", order.Id);
                throw;
            }
        }
        public async Task<bool> NotifyShipmentCreatedAsync(Carrier carrier, string storeApiKey, object payload)
        {
            try
            {
                _logger.LogInformation("Kargo bildirimi gönderiliyor. Carrier: {Carrier}, API: {ApiEndpoint}", carrier.Name, carrier.ApiEndpoint);

                // Örnek: Aras, Yurtiçi, UPS gibi ayrım yapalım
                switch (carrier.IntegrationType)
                {
                    case CarrierIntegrationType.Aras:
                        return await SendToArasAsync(carrier, storeApiKey, payload);

                    case CarrierIntegrationType.Yurtiçi:
                        return await SendToYurticiAsync(carrier, storeApiKey, payload);

                    case CarrierIntegrationType.CustomWebhook:
                        return await SendToCustomWebhookAsync(carrier, storeApiKey, payload);

                    default:
                        _logger.LogWarning("Kargo firması için webhook entegrasyonu tanımlı değil. Carrier: {CarrierName}", carrier.Name);
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo bildirimi sırasında hata oluştu. Carrier: {Carrier}", carrier.Name);
                return false;
            }
        }

        private async Task<bool> SendToArasAsync(Carrier carrier, string apiKey, object payload)
        {
            // Buraya Aras API’ye özel gönderim kodları gelir
            await Task.Delay(50); // Simülasyon
            _logger.LogInformation("Aras Kargo'ya gönderim simüle edildi.");
            return true;
        }

        private async Task<bool> SendToYurticiAsync(Carrier carrier, string apiKey, object payload)
        {
            await Task.Delay(50); // Simülasyon
            _logger.LogInformation("Yurtiçi Kargo'ya gönderim simüle edildi.");
            return true;
        }

        private async Task<bool> SendToCustomWebhookAsync(Carrier carrier, string apiKey, object payload)
        {
            // Basit webhook gönderimi örneği
            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-Store-ApiKey", apiKey);

                var response = await httpClient.PostAsJsonAsync(carrier.ApiEndpoint, payload);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Webhook gönderimi başarısız. StatusCode: {Status}", response.StatusCode);
                    return false;
                }

                _logger.LogInformation("Custom webhook başarıyla gönderildi.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Custom webhook gönderiminde hata.");
                return false;
            }
        }

        public Task<bool> HandleWebhookAsync(string carrierCode, string payload)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateShipmentAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<string> TrackShipmentAsync(string trackingNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelShipmentAsync(string trackingNumber)
        {
            throw new NotImplementedException();
        }
    }
}
