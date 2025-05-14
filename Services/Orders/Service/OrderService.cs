using AutoMapper;
using Data.Dtos.Orders;
using Domain.Orders.Events;
using Entity.Orders;
using Entity.Payments;
using MediatR;
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
        private readonly IMediator _mediator;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IDeliveryAddressRepository deliveryAddressRepository, IBasketRepository basketRepository, IOrderRepository orderRepository, IPaymentRepository paymentRepository, IMapper mapper, IMediator mediator, ILogger<OrderService> logger)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto createDto, int buyerId)
        {
            var basket = await _basketRepository.GetQueryable()
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.BuyerId == buyerId);

            if (basket == null || !basket.Items.Any())
                throw new InvalidOperationException("Sepet boş. Sipariş oluşturulamaz.");

            var address = await _deliveryAddressRepository.GetQueryable()
                .FirstOrDefaultAsync(a => a.Id == createDto.DeliveryAddressId && a.BuyerUserId == buyerId);

            if (address == null)
                throw new InvalidOperationException("Teslimat adresi bulunamadı.");

            var order = new Order
            {
                BuyerId = buyerId,
                DeliveryAddressId = createDto.DeliveryAddressId,
                SelectedCarrierId = createDto.SelectedCarrierId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                OrderNumber = Guid.NewGuid().ToString("N")[..10].ToUpper(),
                Status = createDto.PaymentMethod == PaymentMethod.Online ? OrderStatus.AwaitingPayment : OrderStatus.Created,
                TotalAmount = basket.TotalAmount,
                OrderItems = basket.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    StoreProductImageUrl = item.StoreProductImageUrl
                }).ToList()
            };

            if (basket.Items.Select(x => x.StoreId).Distinct().Count() > 1)
            {
                _logger.LogWarning("Birden fazla mağazaya ait ürün var. Çoklu mağaza sipariş desteği yok.");
                throw new InvalidOperationException("Siparişte birden fazla mağaza ürünü bulunamaz.");
            }

            order.StoreId = basket.Items.First().StoreId;

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

            try
            {
                await _mediator.Publish(new OrderCreatedEvent(order)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OrderCreatedEvent gönderimi sırasında hata oluştu.");
            }

            basket.Items.Clear();
            basket.TotalAmount = 0;
            basket.UpdatedAt = DateTime.UtcNow;
            await _basketRepository.UpdateAsync(basket);

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

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetQueryable()
                .Include(o => o.OrderItems)
                .ToListAsync();
            return _mapper.Map<List<OrderDto>>(orders);
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
