using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantOrderingSystem.Services;
using RestaurantOrderingSystem.Models.Entities;
using RestaurantOrderingSystem.Attributes;
using Microsoft.AspNetCore.Hosting;

namespace RestaurantOrderingSystem.Controllers
{
    [RoleRequired(UserRole.Admin)]
    public class AdminController : Controller
    {
        private readonly ITableService _tableService;
        private readonly IServerService _serverService;
        private readonly IMenuService _menuService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ITableService tableService, IServerService serverService, IMenuService menuService, IWebHostEnvironment webHostEnvironment)
        {
            _tableService = tableService;
            _serverService = serverService;
            _menuService = menuService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        // MENU YÖNETİMİ
        public async Task<IActionResult> MenuItems()
        {
            var menuItems = await _menuService.GetMenuItemsAsync();
            return View(menuItems);
        }

        public async Task<IActionResult> CreateMenuItem()
        {
            var categories = await _menuService.GetCategoriesAsync();
            ViewBag.Categories = categories;
            return View(new MenuItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenuItem(MenuItem menuItem, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    menuItem.ImageUrl = await SaveImage(imageFile);
                }

                await _menuService.CreateMenuItemAsync(menuItem);
                TempData["Success"] = "Menü öğesi başarıyla eklendi!";
                return RedirectToAction("MenuItems");
            }

            var categories = await _menuService.GetCategoriesAsync();
            ViewBag.Categories = categories;
            return View(menuItem);
        }

        public async Task<IActionResult> EditMenuItem(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            var categories = await _menuService.GetCategoriesAsync();
            ViewBag.Categories = categories;
            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMenuItem(MenuItem menuItem, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    menuItem.ImageUrl = await SaveImage(imageFile);
                }
                else
                {
                    // If no new image, retain the existing one from the database
                    var existingMenuItem = await _menuService.GetMenuItemByIdAsync(menuItem.Id);
                    menuItem.ImageUrl = existingMenuItem?.ImageUrl;
                }

                var result = await _menuService.UpdateMenuItemAsync(menuItem);
                if (result)
                {
                    TempData["Success"] = "Menü öğesi başarıyla güncellendi!";
                    return RedirectToAction("MenuItems");
                }
                else
                {
                    TempData["Error"] = "Menü öğesi güncellenemedi!";
                }
            }

            var categories = await _menuService.GetCategoriesAsync();
            ViewBag.Categories = categories;
            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _menuService.DeleteMenuItemAsync(id);
            if (result)
            {
                TempData["Success"] = "Menü öğesi başarıyla silindi!";
            }
            else
            {
                TempData["Error"] = "Menü öğesi silinemedi! Menü öğesi siparişlerde kullanılıyor olabilir.";
            }
            return RedirectToAction("MenuItems");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleMenuItemAvailability(int id)
        {
            var result = await _menuService.ToggleMenuItemAvailabilityAsync(id);
            return Json(new { success = result });
        }

        private async Task<string?> SaveImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "menuitems");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/menuitems/" + uniqueFileName;
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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