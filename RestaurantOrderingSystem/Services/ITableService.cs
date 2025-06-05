using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Services
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table?> GetTableByIdAsync(int id);
        Task<bool> OccupyTableAsync(int tableId);
        Task<bool> FreeTableAsync(int tableId);
        Task<bool> IsTableAvailableAsync(int tableId);
    }
}