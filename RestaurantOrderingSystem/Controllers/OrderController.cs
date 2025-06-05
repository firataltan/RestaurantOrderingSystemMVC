using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystem.Services;
using RestaurantOrderingSystem.Models.DTOs;
using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;

        public OrderController(IOrderService orderService, IMenuService menuService)
        {
            _orderService = orderService;
            _menuService = menuService;
        }

        public async Task<IActionResult> Create()
        {
            var tableId = HttpContext.Session.GetInt32("TableId");
            if (tableId == null)
            {
                TempData["Error"] = "Lütfen önce bir masa seçin.";
                return RedirectToAction("Select", "Table");
            }

            var categories = await _menuService.GetCategoriesAsync();
            ViewBag.TableId = tableId;
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tableId = HttpContext.Session.GetInt32("TableId");
            if (tableId == null || tableId != orderDto.TableId)
            {
                return BadRequest("Geçersiz masa ID'si.");
            }

            var order = await _orderService.CreateOrderAsync(orderDto);
            if (order != null)
            {
                return Json(new { success = true, orderId = order.Id });
            }

            return BadRequest("Sipariş oluşturulamadı.");
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        public async Task<IActionResult> History()
        {
            var tableId = HttpContext.Session.GetInt32("TableId");
            if (tableId == null)
            {
                TempData["Error"] = "Lütfen önce bir masa seçin.";
                return RedirectToAction("Select", "Table");
            }

            var orders = await _orderService.GetOrdersByTableIdAsync(tableId.Value);
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderStatus(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Json(new { status = order.Status.ToString(), statusText = GetStatusText(order.Status) });
        }

        private string GetStatusText(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => "Bekliyor",
                OrderStatus.Preparing => "Hazırlanıyor",
                OrderStatus.Ready => "Hazır",
                OrderStatus.Served => "Servis Edildi",
                OrderStatus.Cancelled => "İptal",
                _ => "Bilinmiyor"
            };
        }
    }
}