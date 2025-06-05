using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingSystem.Models.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public int TableId { get; set; }

        [StringLength(1000)]
        public string? SpecialInstructions { get; set; }

        [Required]
        public List<CreateOrderItemDto> OrderItems { get; set; } = new();
    }

    public class CreateOrderItemDto
    {
        [Required]
        public int MenuItemId { get; set; }

        [Required]
        [Range(1, 50)]
        public int Quantity { get; set; }

        [StringLength(500)]
        public string? SpecialInstructions { get; set; }
    }
}