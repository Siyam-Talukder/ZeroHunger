using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BLL.Validations;

namespace BLL.Models
{
    public class CollectRequestModel
    {
        [Required(ErrorMessage = "Food description is required.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Food description must be between 5 and 500 characters.")]
        public string FoodDescription { get; set; } = null!;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Max preservation time is required.")]
        [FutureDate]
        public DateTime MaxPreservationTime { get; set; }

        [Required(ErrorMessage = "Restaurant ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Restaurant ID must be a valid positive number.")]
        public int RestaurantId { get; set; }

        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}