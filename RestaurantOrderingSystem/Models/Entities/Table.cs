using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingSystem.Models.Entities
{
    public class Table
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Number { get; set; } = string.Empty;

        [Range(1, 20)]
        public int Capacity { get; set; } = 4;

        public bool IsOccupied { get; set; } = false;

        public DateTime? OccupiedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // YENİ: Garson ataması
        public int? ServerId { get; set; }
        public virtual Server? Server { get; set; }

        // YENİ: Masa konumu
        [Range(0, 2000)]
        public int PositionX { get; set; } = 0;

        [Range(0, 2000)]
        public int PositionY { get; set; } = 0;

        // YENİ: Masa durumu
        public TableStatus Status { get; set; } = TableStatus.Available;

        // YENİ: Rezervasyon
        public DateTime? ReservationTime { get; set; }

        [StringLength(100)]
        public string? ReservationName { get; set; }

        // Navigation Properties
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public enum TableStatus
    {
        Available = 1,      // Müsait
        Occupied = 2,       // Dolu
        Reserved = 3,       // Rezerve
        Cleaning = 4,       // Temizleniyor
        OutOfService = 5    // Servis Dışı
    }
}