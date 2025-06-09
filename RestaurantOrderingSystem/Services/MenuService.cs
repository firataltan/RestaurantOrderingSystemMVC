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

        // Admin operations
        public async Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem)
        {
            menuItem.CreatedAt = DateTime.Now;
            menuItem.IsAvailable = true;
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task<bool> UpdateMenuItemAsync(MenuItem menuItem)
        {
            var existingMenuItem = await _context.MenuItems.FindAsync(menuItem.Id);
            if (existingMenuItem == null)
            {
                return false;
            }

            existingMenuItem.Name = menuItem.Name;
            existingMenuItem.Description = menuItem.Description;
            existingMenuItem.Price = menuItem.Price;
            existingMenuItem.CategoryId = menuItem.CategoryId;
            existingMenuItem.ImageUrl = menuItem.ImageUrl;
            existingMenuItem.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return false;
            }

            // Check if the menu item is used in any orders
            var isUsed = await _context.OrderItems.AnyAsync(oi => oi.MenuItemId == id);
            if (isUsed)
            {
                // Instead of deleting, mark as unavailable
                menuItem.IsAvailable = false;
                menuItem.UpdatedAt = DateTime.Now;
            }
            else
            {
                _context.MenuItems.Remove(menuItem);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleMenuItemAvailabilityAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return false;
            }

            menuItem.IsAvailable = !menuItem.IsAvailable;
            menuItem.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}