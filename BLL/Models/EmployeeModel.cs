using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Models
{
    public class EmployeeModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;
    }
}
