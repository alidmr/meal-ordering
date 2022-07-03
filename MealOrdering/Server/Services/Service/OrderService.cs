using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.FilterModels;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly MealOrderingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public OrderService(MealOrderingDbContext context, IMapper mapper, IValidationService validationService)
        {
            _context = context;
            _mapper = mapper;
            _validationService = validationService;
        }


        #region Order Methods


        #region Get

        public async Task<List<OrderDto>> GetOrdersByFilter(OrderListFilterModel Filter)
        {
            var query = _context.Orders.Include(i => i.Supplier).AsQueryable();

            if (Filter.CreatedUserId != Guid.Empty)
                query = query.Where(i => i.CreateUserId == Filter.CreatedUserId);

            if (Filter.CreateDateFirst.HasValue)
                query = query.Where(i => i.CreateDate >= Filter.CreateDateFirst);

            if (Filter.CreateDateLast > DateTime.MinValue)
                query = query.Where(i => i.CreateDate <= Filter.CreateDateLast);


            var list = await query
                      .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();

            return list;
        }


        public async Task<List<OrderDto>> GetOrders(DateTime OrderDate)
        {
            var list = await _context.Orders.Include(i => i.Supplier)
                      .Where(i => i.CreateDate.Date == OrderDate.Date)
                      .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();

            return list;
        }



        public async Task<OrderDto> GetOrderById(Guid Id)
        {
            return await _context.Orders.Where(i => i.Id == Id)
                      .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        #endregion

        #region Post

        public async Task<OrderDto> CreateOrder(OrderDto Order)
        {
            var dbOrder = _mapper.Map<Data.Models.Order>(Order);
            await _context.AddAsync(dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(dbOrder);
        }

        public async Task<OrderDto> UpdateOrder(OrderDto Order)
        {
            var dbOrder = await _context.Orders.FirstOrDefaultAsync(i => i.Id == Order.Id);
            if (dbOrder == null)
                throw new Exception("Order not found");


            if (!_validationService.HasPermission(dbOrder.CreateUserId))
                throw new Exception("You cannot change the order unless you created");

            _mapper.Map(Order, dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(dbOrder);
        }

        public async Task DeleteOrder(Guid OrderId)
        {
            var detailCount = await _context.OrderItems.Where(i => i.OrderId == OrderId).CountAsync();


            if (detailCount > 0)
                throw new Exception($"There are {detailCount} sub items for the order you are trying to delete");

            var order = await _context.Orders.FirstOrDefaultAsync(i => i.Id == OrderId);
            if (order == null)
                throw new Exception("Order not found");


            if (!_validationService.HasPermission(order.CreateUserId))
                throw new Exception("You cannot change the order unless you created");



            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }

        #endregion

        #endregion


        #region OrderItem Methods

        #region Get

        public async Task<List<OrderItemDto>> GetOrderItems(Guid OrderId)
        {
            return await _context.OrderItems.Where(i => i.OrderId == OrderId)
                      .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();
        }

        public async Task<OrderItemDto> GetOrderItemsById(Guid Id)
        {
            return await _context.OrderItems.Include(i => i.Order).Where(i => i.Id == Id)
                      .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        #endregion

        #region Post


        public async Task<OrderItemDto> CreateOrderItem(OrderItemDto OrderItem)
        {
            var order = await _context.Orders
                .Where(i => i.Id == OrderItem.OrderId)
                .Select(i => i.ExpireDate)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception("The main order not found");

            if (order <= DateTime.Now)
                throw new Exception("You cannot create sub order. It is expired !!!");


            var dbOrder = _mapper.Map<Data.Models.OrderItem>(OrderItem);
            await _context.AddAsync(dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemDto>(dbOrder);
        }

        public async Task<OrderItemDto> UpdateOrderItem(OrderItemDto OrderItem)
        {
            var dbOrder = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItem.Id);
            if (dbOrder == null)
                throw new Exception("Order not found");

            _mapper.Map(OrderItem, dbOrder);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderItemDto>(dbOrder);
        }

        public async Task DeleteOrderItem(Guid OrderItemId)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItemId);
            if (orderItem == null)
                throw new Exception("Sub order not found");

            _context.OrderItems.Remove(orderItem);

            await _context.SaveChangesAsync();
        }

        #endregion

        #endregion
    }
}
