using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingSystem.Models.Entities
{
    public class Table
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Number { get; set; } = string.Empty; // "Masa 1", "Masa 2"

        [Range(1, 20)]
        public int Capacity { get; set; } = 4;

        public bool IsOccupied { get; set; } = false;

        public DateTime? OccupiedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}