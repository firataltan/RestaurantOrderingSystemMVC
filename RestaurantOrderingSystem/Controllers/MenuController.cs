using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystem.Services;

namespace RestaurantOrderingSystem.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _menuService.GetCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Category(int id)
        {
            var menuItems = await _menuService.GetMenuItemsByCategoryAsync(id);
            ViewBag.CategoryId = id;
            return View(menuItems);
        }

        public async Task<IActionResult> Details(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems = await _menuService.GetMenuItemsAsync();
            return Json(menuItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItem(int id)
        {
            var menuItem = await _menuService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return Json(menuItem);
        }
    }
}