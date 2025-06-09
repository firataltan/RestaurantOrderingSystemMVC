using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystem.Services;
using RestaurantOrderingSystem.Models.Entities;
using RestaurantOrderingSystem.Models.DTOs;
using RestaurantOrderingSystem.Attributes;

namespace RestaurantOrderingSystem.Controllers
{
    public class KitchenController : Controller
    {
        private readonly IOrderService _orderService;

        public KitchenController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [RoleRequired(UserRole.Admin, UserRole.Kitchen)]
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetActiveOrdersAsync();
            return View(orders);
        }

        [HttpPost]
        [RoleRequired(UserRole.Admin, UserRole.Kitchen)]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrderStatusDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.UpdateOrderStatusAsync(dto.OrderId, dto.Status);
            if (result)
            {
                return Json(new { success = true, message = "Sipariş durumu güncellendi." });
            }

            return BadRequest("Sipariş durumu güncellenemedi.");
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetPendingOrders()
        {
            var orders = await _orderService.GetPendingOrdersAsync();
            return Json(orders.Select(o => new
            {
                id = o.Id,
                tableNumber = o.Table.Number,
                orderTime = o.OrderTime.ToString("HH:mm"),
                status = o.Status.ToString(),
                totalAmount = o.TotalAmount,
                itemCount = o.OrderItems.Count(),
                specialInstructions = o.SpecialInstructions
            }));
        }

        // User'lar için sadece görüntüleme
        [RoleRequired(UserRole.User, UserRole.Kitchen, UserRole.Admin)]
        public async Task<IActionResult> ViewOrders()
        {
            var orders = await _orderService.GetActiveOrdersAsync();
            return View("OrdersReadOnly", orders); // Yeni view
        }
    }
}