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
    }
}