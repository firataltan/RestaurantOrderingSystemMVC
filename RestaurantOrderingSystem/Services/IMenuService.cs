using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<MenuItem>> GetMenuItemsAsync();
        Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId);
        Task<MenuItem?> GetMenuItemByIdAsync(int id);
        Task<bool> IsMenuItemAvailableAsync(int id);

        // Admin operations
        Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem);
        Task<bool> UpdateMenuItemAsync(MenuItem menuItem);
        Task<bool> DeleteMenuItemAsync(int id);
        Task<bool> ToggleMenuItemAvailabilityAsync(int id);
    }
}