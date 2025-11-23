using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace School_Management_System.Application.DTOs.Department
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? HeadOfDepartmentId { get; set; }
        public string? HeadOfDepartmentName { get; set; }
    }
}
