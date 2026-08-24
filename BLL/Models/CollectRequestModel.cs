using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class CollectRequestModel
    {
        [Required]
        public string FoodDescription { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        public DateTime MaxPreservationTime { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
