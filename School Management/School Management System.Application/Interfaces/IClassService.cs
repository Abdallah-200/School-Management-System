using School_Management_System.Application.DTOs.Class;
using School_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Interfaces
{
    public interface IClassService
    {
        Task<Class> CreateClassAsync(CreateClassDTO dto);
        Task<IEnumerable<Class>> GetAllAsync();
    }

}
