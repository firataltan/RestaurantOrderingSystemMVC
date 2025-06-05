using RestaurantOrderingSystem.Models.Entities;
using RestaurantOrderingSystem.Models.DTOs;

namespace RestaurantOrderingSystem.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(CreateOrderDto orderDto);
        Task<IEnumerable<Order>> GetOrdersByTableIdAsync(int tableId);
        Task<IEnumerable<Order>> GetPendingOrdersAsync();
        Task<IEnumerable<Order>> GetActiveOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<decimal> CalculateOrderTotalAsync(int orderId);
    }
}