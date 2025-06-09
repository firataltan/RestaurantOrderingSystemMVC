using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystem.Services;
using RestaurantOrderingSystem.Models.Entities;
using RestaurantOrderingSystem.Attributes;

namespace RestaurantOrderingSystem.Controllers
{
    [RoleRequired(UserRole.Admin)]
    public class AdminController : Controller
    {
        private readonly ITableService _tableService;
        private readonly IServerService _serverService;

        public AdminController(ITableService tableService, IServerService serverService)
        {
            _tableService = tableService;
            _serverService = serverService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // MASA YÖNETİMİ
        public async Task<IActionResult> Tables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return View(tables);
        }

        public async Task<IActionResult> TableLayout()
        {
            var tables = await _tableService.GetAllTablesAsync();
            var servers = await _serverService.GetActiveServersAsync();
            ViewBag.Servers = servers;
            return View(tables);
        }

        [HttpPost]
        public async Task<IActionResult> AssignServer(int tableId, int serverId)
        {
            var result = await _tableService.AssignServerToTableAsync(tableId, serverId);
            if (result)
            {
                TempData["Success"] = "Garson ataması başarılı!";
            }
            else
            {
                TempData["Error"] = "Garson ataması başarısız!";
            }
            return RedirectToAction("TableLayout");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTablePosition(int tableId, int x, int y)
        {
            var result = await _tableService.UpdateTablePositionAsync(tableId, x, y);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTableStatus(int tableId, TableStatus status)
        {
            var result = await _tableService.UpdateTableStatusAsync(tableId, status);
            return Json(new { success = result });
        }

        // GARSON YÖNETİMİ
        public async Task<IActionResult> Servers()
        {
            var servers = await _serverService.GetAllServersAsync();
            return View(servers);
        }

        public IActionResult CreateServer()
        {
            return View(new Server());
        }

        [HttpPost]
        public async Task<IActionResult> CreateServer(Server server)
        {
            if (ModelState.IsValid)
            {
                await _serverService.CreateServerAsync(server);
                TempData["Success"] = "Garson başarıyla eklendi!";
                return RedirectToAction("Servers");
            }
            return View(server);
        }

        public async Task<IActionResult> EditServer(int id)
        {
            var server = await _serverService.GetServerByIdAsync(id);
            if (server == null)
            {
                return NotFound();
            }
            return View(server);
        }

        [HttpPost]
        public async Task<IActionResult> EditServer(Server server)
        {
            if (ModelState.IsValid)
            {
                await _serverService.UpdateServerAsync(server);
                TempData["Success"] = "Garson bilgileri güncellendi!";
                return RedirectToAction("Servers");
            }
            return View(server);
        }

        public async Task<IActionResult> ServerDetails(int id)
        {
            var server = await _serverService.GetServerByIdAsync(id);
            if (server == null)
            {
                return NotFound();
            }
            return View(server);
        }

        // MASA OLUŞTURMA/DÜZENLEME
        public IActionResult CreateTable()
        {
            return View(new Table());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable(Table table)
        {
            if (ModelState.IsValid)
            {
                await _tableService.CreateTableAsync(table);
                TempData["Success"] = "Masa başarıyla eklendi!";
                return RedirectToAction("Tables");
            }
            return View(table);
        }
        public async Task<IActionResult> EditTable(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                TempData["Error"] = "Masa bulunamadı.";
                return RedirectToAction("Tables");
            }
            return View(table);
        }

        [HttpPost]
        public async Task<IActionResult> EditTable(Table table)
        {
            if (ModelState.IsValid)
            {
                var result = await _tableService.UpdateTableAsync(table);
                if (result)
                {
                    TempData["Success"] = "Masa bilgileri başarıyla güncellendi!";
                    return RedirectToAction("Tables");
                }
                else
                {
                    TempData["Error"] = "Masa güncellenemedi!";
                }
            }

            // Model hatası varsa formu tekrar göster
            return View(table);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTableAsync(id);
            if (result)
            {
                TempData["Success"] = "Masa başarıyla silindi!";
            }
            else
            {
                TempData["Error"] = "Masa silinemedi! Masa dolu olabilir.";
            }
            return RedirectToAction("Tables");
        }
    }
}