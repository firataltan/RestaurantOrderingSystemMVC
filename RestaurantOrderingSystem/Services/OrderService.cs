using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.Models.Entities;
using RestaurantOrderingSystem.Models.DTOs;

namespace RestaurantOrderingSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var table = await _context.Tables.FindAsync(orderDto.TableId);
            if (table == null) return null;

            var order = new Order
            {
                TableId = orderDto.TableId,
                SpecialInstructions = orderDto.SpecialInstructions,
                Status = OrderStatus.Pending,
                OrderTime = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Order items ekleme
            decimal totalAmount = 0;
            foreach (var item in orderDto.OrderItems)
            {
                var menuItem = await _context.MenuItems.FindAsync(item.MenuItemId);
                if (menuItem != null && menuItem.IsAvailable)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        MenuItemId = item.MenuItemId,
                        Quantity = item.Quantity,
                        UnitPrice = menuItem.Price,
                        SpecialInstructions = item.SpecialInstructions
                    };

                    _context.OrderItems.Add(orderItem);
                    totalAmount += orderItem.SubTotal;
                }
            }

            order.TotalAmount = totalAmount;
            await _context.SaveChangesAsync();

            return await GetOrderByIdAsync(order.Id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByTableIdAsync(int tableId)
        {
            return await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.TableId == tableId)
                .OrderByDescending(o => o.OrderTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetPendingOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Preparing)
                .OrderBy(o => o.OrderTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetActiveOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.Status != OrderStatus.Served && o.Status != OrderStatus.Cancelled)
                .OrderBy(o => o.OrderTime)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                        .ThenInclude(mi => mi.Category)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                if (status == OrderStatus.Served)
                {
                    order.CompletedAt = DateTime.Now;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            var orderItems = await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();

            return orderItems.Sum(oi => oi.SubTotal);
        }
    }
}