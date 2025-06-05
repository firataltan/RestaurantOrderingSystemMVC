using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Eğer veritabanı yoksa oluştur
            context.Database.EnsureCreated();

            // Eğer veri varsa işlem yapma
            if (context.Categories.Any())
            {
                return; // Veri zaten var
            }

            // Ek test verileri buraya eklenebilir
            // Seed data zaten OnModelCreating'de tanımlı
        }
    }
}