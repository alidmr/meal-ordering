using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.FilterModels;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService OrderService)
        {
            _orderService = OrderService;
        }



        #region Order Methods

        [HttpGet("OrderById/{Id}")]
        public async Task<ServiceResponse<OrderDto>> GetOrderById(Guid Id)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await _orderService.GetOrderById(Id)
            };
        }

        [HttpGet("OrdersByDate")]
        public async Task<ServiceResponse<List<OrderDto>>> GetOrder(DateTime orderDate)
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await _orderService.GetOrders(orderDate)
            };
        }

        [HttpPost("OrdersByFilter")]
        public async Task<ServiceResponse<List<OrderDto>>> GetOrdersByFilter([FromBody] OrderListFilterModel filter)
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await _orderService.GetOrdersByFilter(filter)
            };
        }

        [HttpGet("TodaysOrder")]
        public async Task<ServiceResponse<List<OrderDto>>> GetTodaysOrder()
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await _orderService.GetOrders(DateTime.Now)
            };
        }

        [HttpPost("CreateOrder")]
        public async Task<ServiceResponse<OrderDto>> CreateOrder(OrderDto order)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await _orderService.CreateOrder(order)
            };
        }

        [HttpPost("UpdateOrder")]
        public async Task<ServiceResponse<OrderDto>> UpdateOrder(OrderDto order)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await _orderService.UpdateOrder(order)
            };
        }

        [HttpPost("DeleteOrder")]
        public async Task<BaseResponse> DeleteOrder([FromBody] Guid orderId)
        {
            await _orderService.DeleteOrder(orderId);
            return new BaseResponse();
        }

        [HttpGet("DeleteOrder/{orderId}")]
        public async Task<BaseResponse> DeleteOrderFromQueryString(Guid orderId)
        {
            await _orderService.DeleteOrder(orderId);
            return new BaseResponse();
        }

        #endregion

        #region OrderItem Methods

        #region Get

        [HttpGet("OrderItemsById/{id}")]
        public async Task<ServiceResponse<OrderItemDto>> GetOrderItemsById(Guid id)
        {
            return new ServiceResponse<OrderItemDto>()
            {
                Value = await _orderService.GetOrderItemsById(id)
            };
        }

        #endregion


        [HttpPost("CreateOrderItem")]
        public async Task<ServiceResponse<OrderItemDto>> CreateOrderItem(OrderItemDto OrderItem)
        {
            return new ServiceResponse<OrderItemDto>()
            {
                Value = await _orderService.CreateOrderItem(OrderItem)
            };
        }

        [HttpPost("UpdateOrderItem")]
        public async Task<ServiceResponse<OrderItemDto>> UpdateOrderItem(OrderItemDto OrderItem)
        {
            return new ServiceResponse<OrderItemDto>()
            {
                Value = await _orderService.UpdateOrderItem(OrderItem)
            };
        }


        [HttpPost("DeleteOrderItem")]
        public async Task<BaseResponse> DeleteOrderItem([FromBody] Guid OrderItemId)
        {
            await _orderService.DeleteOrderItem(OrderItemId);
            return new BaseResponse();
        }

        [HttpGet("OrderItems")]
        public async Task<ServiceResponse<List<OrderItemDto>>> GetOrderItems(Guid OrderId)
        {
            return new ServiceResponse<List<OrderItemDto>>()
            {
                Value = await _orderService.GetOrderItems(OrderId)
            };
        }

        #endregion
    }
}
