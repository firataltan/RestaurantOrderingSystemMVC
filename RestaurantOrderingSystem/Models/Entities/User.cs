using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingSystem.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty; // Hash'lenmiş

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(100)]
        public string? FullName { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastLoginAt { get; set; }

        // Navigation Properties
        public int? CurrentTableId { get; set; } // User için aktif masa
        public virtual Table? CurrentTable { get; set; }
    }

    public enum UserRole
    {
        User = 1,       // Müşteri
        Kitchen = 2,    // Mutfak personeli  
        Admin = 3       // Yönetici
    }
} 