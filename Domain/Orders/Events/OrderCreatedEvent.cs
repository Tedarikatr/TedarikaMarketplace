using Entity.Orders;
using MediatR;

namespace Domain.Orders.Events
{
    public class OrderCreatedEvent : INotification
    {
        public Order Order { get; }

        public OrderCreatedEvent(Order order)
        {
            Order = order;
        }
    }
}
