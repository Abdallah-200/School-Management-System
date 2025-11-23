using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.DTOs.Department
{
    public class CreateDepartmentDTO
    {

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int HeadOfDepartmentId { get; set; }
    }
}
