using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystem.Services;

namespace RestaurantOrderingSystem.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        public async Task<IActionResult> Select()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return View(tables);
        }

        [HttpPost]
        public async Task<IActionResult> Occupy(int tableId)
        {
            var result = await _tableService.OccupyTableAsync(tableId);
            if (result)
            {
                // Session'a masa ID'sini kaydet
                HttpContext.Session.SetInt32("TableId", tableId);
                return RedirectToAction("Index", "Menu");
            }

            TempData["Error"] = "Masa işgal edilemedi. Masa zaten dolu olabilir.";
            return RedirectToAction("Select");
        }

        [HttpPost]
        public async Task<IActionResult> Free(int tableId)
        {
            var result = await _tableService.FreeTableAsync(tableId);
            if (result)
            {
                // Session'dan masa ID'sini kaldır
                HttpContext.Session.Remove("TableId");
                TempData["Success"] = "Masa başarıyla boşaltıldı.";
            }
            else
            {
                TempData["Error"] = "Masa boşaltılamadı.";
            }

            return RedirectToAction("Select");
        }

        public async Task<IActionResult> Status()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return View(tables);
        }

        [HttpGet]
        public async Task<IActionResult> CheckAvailability(int tableId)
        {
            var isAvailable = await _tableService.IsTableAvailableAsync(tableId);
            return Json(new { isAvailable });
        }
    }
}