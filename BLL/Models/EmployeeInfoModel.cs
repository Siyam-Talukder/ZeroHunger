using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.Text;

namespace BLL.Models
{
    public class EmployeeInfoModel
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a valid positive number.")]
        public int EmployeeId { get; set; }
    }
}