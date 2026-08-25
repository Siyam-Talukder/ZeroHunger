using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BLL.Validations;

namespace BLL.Models
{
    public class EmployeeModel
    {
        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [BdPhone]
        public string Phone { get; set; } = null!;
    }
}