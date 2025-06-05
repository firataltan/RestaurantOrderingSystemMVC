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

        // YENİ METODLAR
        Task<bool> AssignServerToTableAsync(int tableId, int serverId);
        Task<bool> UnassignServerFromTableAsync(int tableId);
        Task<bool> UpdateTableStatusAsync(int tableId, TableStatus status);
        Task<bool> UpdateTablePositionAsync(int tableId, int x, int y);
        Task<Table> CreateTableAsync(Table table);
        Task<bool> UpdateTableAsync(Table table);
        Task<bool> DeleteTableAsync(int tableId);
        Task<IEnumerable<Table>> GetTablesByServerAsync(int serverId);
    }
}