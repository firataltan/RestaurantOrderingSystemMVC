using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Services
{
    public class TableService : ITableService
    {
        private readonly ApplicationDbContext _context;

        public TableService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _context.Tables
                .OrderBy(t => t.Number)
                .ToListAsync();
        }

        public async Task<Table?> GetTableByIdAsync(int id)
        {
            return await _context.Tables
                .Include(t => t.Orders)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> OccupyTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null && !table.IsOccupied)
            {
                table.IsOccupied = true;
                table.OccupiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> FreeTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null && table.IsOccupied)
            {
                table.IsOccupied = false;
                table.OccupiedAt = null;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> IsTableAvailableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            return table != null && !table.IsOccupied;
        }
    }
}