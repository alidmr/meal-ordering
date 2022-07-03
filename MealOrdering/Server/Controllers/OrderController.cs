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
        public async Task<ServiceResponse<List<OrderDto>>> GetOrder(DateTime OrderDate)
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await _orderService.GetOrders(OrderDate)
            };
        }

        [HttpPost("OrdersByFilter")]
        public async Task<ServiceResponse<List<OrderDto>>> GetOrdersByFilter([FromBody] OrderListFilterModel Filter)
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await _orderService.GetOrdersByFilter(Filter)
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
        public async Task<ServiceResponse<OrderDto>> CreateOrder(OrderDto Order)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await _orderService.CreateOrder(Order)
            };
        }

        [HttpPost("UpdateOrder")]
        public async Task<ServiceResponse<OrderDto>> UpdateOrder(OrderDto Order)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await _orderService.UpdateOrder(Order)
            };
        }

        [HttpPost("DeleteOrder")]
        public async Task<BaseResponse> DeleteOrder([FromBody] Guid OrderId)
        {
            await _orderService.DeleteOrder(OrderId);
            return new BaseResponse();
        }

        [HttpGet("DeleteOrder/{OrderId}")]
        public async Task<BaseResponse> DeleteOrderFromQueryString(Guid OrderId)
        {
            await _orderService.DeleteOrder(OrderId);
            return new BaseResponse();
        }

        #endregion

        #region OrderItem Methods

        #region Get

        [HttpGet("OrderItemsById/{Id}")]
        public async Task<ServiceResponse<OrderItemDto>> GetOrderItemsById(Guid Id)
        {
            return new ServiceResponse<OrderItemDto>()
            {
                Value = await _orderService.GetOrderItemsById(Id)
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
