using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Services
{
    public class ServerService : IServerService
    {
        private readonly ApplicationDbContext _context;

        public ServerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Server>> GetAllServersAsync()
        {
            return await _context.Servers
                .Include(s => s.AssignedTables)
                .OrderBy(s => s.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Server>> GetActiveServersAsync()
        {
            return await _context.Servers
                .Include(s => s.AssignedTables)
                .Where(s => s.IsActive)
                .OrderBy(s => s.FirstName)
                .ToListAsync();
        }

        public async Task<Server?> GetServerByIdAsync(int id)
        {
            return await _context.Servers
                .Include(s => s.AssignedTables)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Server> CreateServerAsync(Server server)
        {
            _context.Servers.Add(server);
            await _context.SaveChangesAsync();
            return server;
        }

        public async Task<bool> UpdateServerAsync(Server server)
        {
            _context.Servers.Update(server);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteServerAsync(int id)
        {
            var server = await _context.Servers.FindAsync(id);
            if (server != null)
            {
                server.IsActive = false; // Soft delete
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<Table>> GetServerTablesAsync(int serverId)
        {
            return await _context.Tables
                .Where(t => t.ServerId == serverId)
                .OrderBy(t => t.Number)
                .ToListAsync();
        }
    }
}