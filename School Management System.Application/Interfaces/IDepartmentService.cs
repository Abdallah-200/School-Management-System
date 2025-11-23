using School_Management_System.Application.DTOs.Department;
using School_Management_System.Application.DTOs.User;
using School_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> CreateDepartmentAsync(CreateDepartmentDTO dto);
        Task<Department?> GetDepartmentByIdAsync(int id);
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<DepartmentDTO> UpdateDepartmentAsync(int id, UpdateDepartmentDTO dto);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
