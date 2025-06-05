using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category Configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // MenuItem Configuration
            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ImageUrl).HasMaxLength(500);

                // Foreign Key Relationship
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.MenuItems)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Table Configuration
            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Number).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Number).IsUnique();
            });

            // Order Configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SpecialInstructions).HasMaxLength(1000);

                // Foreign Key Relationship
                entity.HasOne(e => e.Table)
                      .WithMany(t => t.Orders)
                      .HasForeignKey(e => e.TableId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // OrderItem Configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SpecialInstructions).HasMaxLength(500);

                // Foreign Key Relationships
                entity.HasOne(e => e.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.MenuItem)
                      .WithMany(m => m.OrderItems)
                      .HasForeignKey(e => e.MenuItemId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed Data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Çorbalar", Description = "Sıcak çorbalar", CreatedAt = DateTime.Now },
                new Category { Id = 2, Name = "Ana Yemekler", Description = "Ana yemek çeşitleri", CreatedAt = DateTime.Now },
                new Category { Id = 3, Name = "İçecekler", Description = "Sıcak ve soğuk içecekler", CreatedAt = DateTime.Now },
                new Category { Id = 4, Name = "Tatlılar", Description = "Tatlı çeşitleri", CreatedAt = DateTime.Now }
            );

            // Tables
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, Number = "Masa 1", Capacity = 4, CreatedAt = DateTime.Now },
                new Table { Id = 2, Number = "Masa 2", Capacity = 4, CreatedAt = DateTime.Now },
                new Table { Id = 3, Number = "Masa 3", Capacity = 6, CreatedAt = DateTime.Now },
                new Table { Id = 4, Number = "Masa 4", Capacity = 2, CreatedAt = DateTime.Now },
                new Table { Id = 5, Number = "Masa 5", Capacity = 8, CreatedAt = DateTime.Now }
            );

            // Menu Items
            modelBuilder.Entity<MenuItem>().HasData(
                // Çorbalar
                new MenuItem { Id = 1, Name = "Mercimek Çorbası", Description = "Geleneksel mercimek çorbası", Price = 25.00m, CategoryId = 1, CreatedAt = DateTime.Now },
                new MenuItem { Id = 2, Name = "Tavuk Çorbası", Description = "Ev yapımı tavuk çorbası", Price = 30.00m, CategoryId = 1, CreatedAt = DateTime.Now },

                // Ana Yemekler
                new MenuItem { Id = 3, Name = "Izgara Tavuk", Description = "Baharatlarla marine edilmiş izgara tavuk", Price = 85.00m, CategoryId = 2, CreatedAt = DateTime.Now },
                new MenuItem { Id = 4, Name = "Köfte", Description = "Ev yapımı köfte", Price = 75.00m, CategoryId = 2, CreatedAt = DateTime.Now },
                new MenuItem { Id = 5, Name = "Balık Izgara", Description = "Günün taze balığı", Price = 120.00m, CategoryId = 2, CreatedAt = DateTime.Now },

                // İçecekler
                new MenuItem { Id = 6, Name = "Çay", Description = "Türk çayı", Price = 10.00m, CategoryId = 3, CreatedAt = DateTime.Now },
                new MenuItem { Id = 7, Name = "Kahve", Description = "Türk kahvesi", Price = 20.00m, CategoryId = 3, CreatedAt = DateTime.Now },
                new MenuItem { Id = 8, Name = "Kola", Description = "Soğuk kola", Price = 15.00m, CategoryId = 3, CreatedAt = DateTime.Now },

                // Tatlılar
                new MenuItem { Id = 9, Name = "Baklava", Description = "Antep fıstıklı baklava", Price = 45.00m, CategoryId = 4, CreatedAt = DateTime.Now },
                new MenuItem { Id = 10, Name = "Sütlaç", Description = "Ev yapımı sütlaç", Price = 35.00m, CategoryId = 4, CreatedAt = DateTime.Now }
            );
        }
    }
}