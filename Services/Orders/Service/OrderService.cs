using AutoMapper;
using Microsoft.Extensions.Logging;
using Repository.Baskets.IRepositorys;
using Repository.DeliveryAddresses.IRepositorys;
using Repository.Orders.IRepositorys;
using Repository.Payments.IRepositorys;

namespace Services.Orders.Service
{
    internal class OrderService
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
    }
}
