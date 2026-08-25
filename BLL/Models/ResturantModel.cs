using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BLL.Validations;

namespace BLL.Models
{
    public class ResturantModel
    {
        [Required(ErrorMessage = "Restaurant name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 250 characters.")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [BdPhone]
        public string Phone { get; set; } = null!;
    }
}