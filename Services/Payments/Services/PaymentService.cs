using AutoMapper;
using Data.Dtos.Payments;
using Entity.Payments;
using Microsoft.Extensions.Logging;
using Repository.Payments.IRepositorys;
using Services.Payments.IServices;

namespace Services.Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentRepository paymentRepo, IMapper mapper, ILogger<PaymentService> logger)
        {
            _paymentRepo = paymentRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Payment> CreatePaymentAsync(int buyerId, int orderId, PaymentCreateDto dto)
        {
            try
            {
                var payment = new Payment
                {
                    BuyerId = buyerId,
                    OrderId = orderId,
                    PaymentMethod = dto.PaymentMethod,
                    TotalAmount = dto.TotalAmount,
                    PaidAmount = dto.TotalAmount,
                    Currency = dto.Currency,
                    Status = PaymentStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };

                await _paymentRepo.AddAsync(payment);
                _logger.LogInformation("Ödeme kaydı oluşturuldu. BuyerId: {BuyerId}, OrderId: {OrderId}, Method: {Method}", buyerId, orderId, dto.PaymentMethod);
                return payment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödeme oluşturulurken hata oluştu.");
                throw;
            }
        }

        public async Task<PaymentDto> GetPaymentByOrderIdAsync(int orderId)
        {
            var payment = await _paymentRepo.SingleOrDefaultAsync(p => p.OrderId == orderId);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<List<PaymentDto>> GetPaymentsByBuyerAsync(int buyerId)
        {
            var payments = await _paymentRepo.FindAsync(p => p.BuyerId == buyerId);
            return _mapper.Map<List<PaymentDto>>(payments.OrderByDescending(p => p.CreatedAt));
        }

        public async Task<bool> MarkAsCompletedAsync(int paymentId, string? reference = null)
        {
            var payment = await _paymentRepo.GetByIdAsync(paymentId);
            if (payment == null)
            {
                _logger.LogWarning("Tamamlanmak istenen ödeme bulunamadı. PaymentId: {Id}", paymentId);
                return false;
            }

            payment.Status = PaymentStatus.Completed;
            payment.PaymentReference = reference;
            await _paymentRepo.UpdateAsync(payment);

            _logger.LogInformation("Ödeme başarıyla tamamlandı. PaymentId: {Id}, Reference: {Ref}", paymentId, reference);
            return true;
        }

        public async Task<bool> MarkAsFailedAsync(int paymentId, string errorMessage, string? errorCode = null)
        {
            var payment = await _paymentRepo.GetByIdAsync(paymentId);
            if (payment == null)
            {
                _logger.LogWarning("Başarısız olarak işaretlenmek istenen ödeme bulunamadı. PaymentId: {Id}", paymentId);
                return false;
            }

            payment.Status = PaymentStatus.Failed;
            payment.ErrorMessage = errorMessage;
            payment.ErrorCode = errorCode;
            await _paymentRepo.UpdateAsync(payment);

            _logger.LogWarning("Ödeme başarısız olarak işaretlendi. PaymentId: {Id}, Error: {Error}", paymentId, errorMessage);
            return true;
        }
    }
}
