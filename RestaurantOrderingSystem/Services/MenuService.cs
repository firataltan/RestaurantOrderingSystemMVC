using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Services
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories
                .Include(c => c.MenuItems.Where(m => m.IsAvailable))
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync()
        {
            return await _context.MenuItems
                .Include(m => m.Category)
                .Where(m => m.IsAvailable)
                .OrderBy(m => m.Category.Name)
                .ThenBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId)
        {
            return await _context.MenuItems
                .Include(m => m.Category)
                .Where(m => m.CategoryId == categoryId && m.IsAvailable)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
        {
            return await _context.MenuItems
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id && m.IsAvailable);
        }

        public async Task<bool> IsMenuItemAvailableAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            return menuItem?.IsAvailable ?? false;
        }
    }
}