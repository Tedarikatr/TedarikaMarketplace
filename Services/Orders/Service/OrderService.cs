using AutoMapper;
using Data.Dtos.Orders;
using Entity.Orders;
using Entity.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Baskets.IRepositorys;
using Repository.DeliveryAddresses.IRepositorys;
using Repository.Orders.IRepositorys;
using Repository.Payments.IRepositorys;
using Services.Orders.IServices;

namespace Services.Orders.Service
{
    public class OrderService : IOrderService
    {
        private readonly IDeliveryAddressRepository _deliveryAddressRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IBasketRepository basketRepository,
            IOrderRepository orderRepository,
            IPaymentRepository paymentRepository,
            IDeliveryAddressRepository deliveryAddressRepository,
            IMapper mapper,
            ILogger<OrderService> logger)
        {
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _deliveryAddressRepository = deliveryAddressRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto createDto, int buyerId)
        {
            _logger.LogInformation("Siparis olusturuluyor... BuyerId: {BuyerId}", buyerId);

            var basket = await _basketRepository.GetQueryable()
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.BuyerId == buyerId);

            if (basket == null || !basket.Items.Any())
                throw new InvalidOperationException("Sepet bos. Siparis olusturulamaz.");

            var address = await _deliveryAddressRepository.GetQueryable()
                .FirstOrDefaultAsync(a => a.Id == createDto.DeliveryAddressId && a.BuyerUserId == buyerId);

            if (address == null)
            {
                _logger.LogWarning("Gecersiz teslimat adresi. AddressId: {AddressId}, BuyerId: {BuyerId}",
                    createDto.DeliveryAddressId, buyerId);
                throw new InvalidOperationException("Secilen teslimat adresi bulunamadi.");
            }

            var order = new Order
            {
                BuyerId = buyerId,
                StoreId = createDto.StoreId,
                DeliveryAddressId = createDto.DeliveryAddressId,
                SelectedCarrierId = createDto.SelectedCarrierId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                OrderNumber = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(),
                Status = createDto.PaymentMethod == PaymentMethod.Online ? OrderStatus.AwaitingPayment : OrderStatus.Created,
                OrderItems = new List<OrderItem>(),
                TotalAmount = basket.TotalAmount
            };

            foreach (var item in basket.Items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    StoreProductImageUrl = item.StoreProductImageUrl
                });
            }

            if (createDto.PaymentMethod == PaymentMethod.Online)
            {
                var payment = new Payment
                {
                    BuyerId = buyerId,
                    TotalAmount = order.TotalAmount,
                    PaidPrice = 0,
                    CreatedAt = DateTime.UtcNow,
                    PaymentMethod = createDto.PaymentMethod,
                    Status = PaymentStatus.Pending,
                    OrderNumber = order.OrderNumber,
                    Currency = "₺"
                };

                await _paymentRepository.AddAsync(payment);
                order.Payment = payment;
            }

            await _orderRepository.AddAsync(order);
            _logger.LogInformation("Siparis basariyla olusturuldu. OrderId: {OrderId}", order.Id);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId, int buyerId)
        {
            var order = await _orderRepository.GetQueryable()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.BuyerId == buyerId);

            if (order == null)
                throw new UnauthorizedAccessException("Siparis bulunamadi veya erisim izniniz yok.");

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderListDto>> GetOrdersByBuyerIdAsync(int buyerId)
        {
            var orders = await _orderRepository.GetQueryable()
                .Where(o => o.BuyerId == buyerId)
                .ToListAsync();

            return _mapper.Map<List<OrderListDto>>(orders);
        }

        public async Task<List<OrderListDto>> GetOrdersByStoreIdAsync(int storeId)
        {
            var orders = await _orderRepository.GetQueryable()
                .Where(o => o.StoreId == storeId)
                .ToListAsync();

            return _mapper.Map<List<OrderListDto>>(orders);
        }

        public async Task<OrderStatus> GetOrderStatusAsync(int orderId, int buyerId)
        {
            var order = await _orderRepository.GetQueryable()
                .FirstOrDefaultAsync(o => o.Id == orderId && o.BuyerId == buyerId);

            if (order == null)
                throw new UnauthorizedAccessException("Siparis bilgisine erisilemiyor.");

            return order.Status;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return false;

            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;

            return await _orderRepository.UpdateBoolAsync(order);
        }

        public async Task<bool> CancelOrderAsync(int orderId, int buyerId)
        {
            var order = await _orderRepository.GetQueryable()
                .FirstOrDefaultAsync(o => o.Id == orderId && o.BuyerId == buyerId);

            if (order == null)
                return false;

            if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Delivered)
                throw new InvalidOperationException("Kargoya verilen siparis iptal edilemez.");

            order.Status = OrderStatus.Cancelled;
            order.UpdatedAt = DateTime.UtcNow;

            return await _orderRepository.UpdateBoolAsync(order);
        }

        public async Task<bool> IsOrderPaidAsync(int orderId)
        {
            var order = await _orderRepository.GetQueryable()
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            return order?.Payment?.Status == PaymentStatus.Completed;
        }
    }
}
