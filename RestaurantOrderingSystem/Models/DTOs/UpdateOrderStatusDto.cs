using RestaurantOrderingSystem.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingSystem.Models.DTOs
{
    public class UpdateOrderStatusDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public OrderStatus Status { get; set; }
    }
}