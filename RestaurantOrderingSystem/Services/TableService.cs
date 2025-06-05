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

        // MEVCUT METODLAR
        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _context.Tables
                .Include(t => t.Server) // Garson bilgisini de getir
                .OrderBy(t => t.Number)
                .ToListAsync();
        }

        public async Task<Table?> GetTableByIdAsync(int id)
        {
            return await _context.Tables
                .Include(t => t.Orders)
                .Include(t => t.Server) // Garson bilgisini de getir
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> OccupyTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null && !table.IsOccupied)
            {
                table.IsOccupied = true;
                table.OccupiedAt = DateTime.Now;
                table.Status = TableStatus.Occupied; // Yeni status alanı
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
                table.Status = TableStatus.Available; // Yeni status alanı
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

        // YENİ METODLAR - ADMİN YÖNETİMİ İÇİN
        public async Task<bool> AssignServerToTableAsync(int tableId, int serverId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            var server = await _context.Servers.FindAsync(serverId);

            if (table != null && server != null && server.IsActive)
            {
                table.ServerId = serverId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UnassignServerFromTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null)
            {
                table.ServerId = null;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateTableStatusAsync(int tableId, TableStatus status)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null)
            {
                table.Status = status;

                // Status'a göre IsOccupied alanını da güncelle
                if (status == TableStatus.Occupied)
                {
                    table.IsOccupied = true;
                    if (!table.OccupiedAt.HasValue)
                        table.OccupiedAt = DateTime.Now;
                }
                else if (status == TableStatus.Available)
                {
                    table.IsOccupied = false;
                    table.OccupiedAt = null;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateTablePositionAsync(int tableId, int x, int y)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null)
            {
                table.PositionX = x;
                table.PositionY = y;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Table> CreateTableAsync(Table table)
        {
            table.CreatedAt = DateTime.Now;
            table.Status = TableStatus.Available;
            table.IsOccupied = false;

            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return table;
        }

        public async Task<bool> UpdateTableAsync(Table table)
        {
            var existingTable = await _context.Tables.FindAsync(table.Id);
            if (existingTable != null)
            {
                existingTable.Number = table.Number;
                existingTable.Capacity = table.Capacity;
                existingTable.PositionX = table.PositionX;
                existingTable.PositionY = table.PositionY;
                existingTable.Status = table.Status;
                existingTable.ServerId = table.ServerId;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table != null)
            {
                // Eğer masa dolu ise silinmesine izin verme
                if (table.IsOccupied)
                {
                    return false;
                }

                // Soft delete - sadece servis dışı yap
                table.Status = TableStatus.OutOfService;
                await _context.SaveChangesAsync();
                return true;

                // Hard delete yapmak istersen:
                // _context.Tables.Remove(table);
                // await _context.SaveChangesAsync();
                // return true;
            }
            return false;
        }

        public async Task<IEnumerable<Table>> GetTablesByServerAsync(int serverId)
        {
            return await _context.Tables
                .Include(t => t.Server)
                .Where(t => t.ServerId == serverId)
                .OrderBy(t => t.Number)
                .ToListAsync();
        }
    }
}