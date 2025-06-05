using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Services
{
    public interface IServerService
    {
        Task<IEnumerable<Server>> GetAllServersAsync();
        Task<IEnumerable<Server>> GetActiveServersAsync();
        Task<Server?> GetServerByIdAsync(int id);
        Task<Server> CreateServerAsync(Server server);
        Task<bool> UpdateServerAsync(Server server);
        Task<bool> DeleteServerAsync(int id);
        Task<IEnumerable<Table>> GetServerTablesAsync(int serverId);
    }
}