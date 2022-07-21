using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.FilterModels;

namespace MealOrdering.Server.Services.Infrastructure
{
    public interface IOrderService
    {
        public Task<OrderDto> CreateOrder(OrderDto order);

        public Task<OrderDto> UpdateOrder(OrderDto order);

        public Task DeleteOrder(Guid orderId);

        public Task<List<OrderDto>> GetOrders(DateTime orderDate);

        public Task<List<OrderDto>> GetOrdersByFilter(OrderListFilterModel filter);

        public Task<OrderDto> GetOrderById(Guid id);



        public Task<OrderItemDto> CreateOrderItem(OrderItemDto orderItem);

        public Task<OrderItemDto> UpdateOrderItem(OrderItemDto orderItem);

        public Task<List<OrderItemDto>> GetOrderItems(Guid orderId);

        public Task<OrderItemDto> GetOrderItemsById(Guid id);

        public Task DeleteOrderItem(Guid orderItemId);
    }
}
