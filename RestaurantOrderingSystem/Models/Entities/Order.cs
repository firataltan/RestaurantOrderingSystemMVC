using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrderingSystem.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int TableId { get; set; }

        public DateTime OrderTime { get; set; } = DateTime.Now;

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(1000)]
        public string? SpecialInstructions { get; set; }

        public DateTime? CompletedAt { get; set; }

        // Navigation Properties
        public virtual Table Table { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public enum OrderStatus
    {
        Pending = 1,      // Bekliyor
        Preparing = 2,    // Hazırlanıyor
        Ready = 3,        // Hazır
        Served = 4,       // Servis Edildi
        Cancelled = 5     // İptal
    }
}