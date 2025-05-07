using Data.Dtos.Payments;
using Entity.Payments;

namespace Services.Payments.IServices
{

    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(int buyerId, int orderId, PaymentCreateDto dto);
        Task<PaymentDto> GetPaymentByOrderIdAsync(int orderId);
        Task<List<PaymentDto>> GetPaymentsByBuyerAsync(int buyerId);
        Task<bool> MarkAsCompletedAsync(int paymentId, string? reference = null);
        Task<bool> MarkAsFailedAsync(int paymentId, string errorMessage, string? errorCode = null);
    }
}
