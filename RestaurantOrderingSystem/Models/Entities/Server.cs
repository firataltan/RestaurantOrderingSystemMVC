using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingSystem.Models.Entities
{
    public class Server
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? EmployeeCode { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime HireDate { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual ICollection<Table> AssignedTables { get; set; } = new List<Table>();

        // Computed Property
        public string FullName => $"{FirstName} {LastName}";
    }
}