using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.FilterModels;

namespace MealOrdering.Server.Services.Infrastructure
{
    public interface IOrderService
    {
        public Task<OrderDto> CreateOrder(OrderDto Order);

        public Task<OrderDto> UpdateOrder(OrderDto Order);

        public Task DeleteOrder(Guid OrderId);

        public Task<List<OrderDto>> GetOrders(DateTime OrderDate);

        public Task<List<OrderDto>> GetOrdersByFilter(OrderListFilterModel Filter);

        public Task<OrderDto> GetOrderById(Guid Id);



        public Task<OrderItemDto> CreateOrderItem(OrderItemDto OrderItem);

        public Task<OrderItemDto> UpdateOrderItem(OrderItemDto OrderItem);

        public Task<List<OrderItemDto>> GetOrderItems(Guid OrderId);

        public Task<OrderItemDto> GetOrderItemsById(Guid Id);

        public Task DeleteOrderItem(Guid OrderItemId);
    }
}
