using School_Management_System.Application.DTOs.Assignment;
using School_Management_System.Domain.Entities;


namespace School_Management_System.Application.Interfaces
{
    public interface IAssignmentService
    {
        Task<Assignment> CreateAssignmentAsync(CreateAssignmentDTO dto);
        Task<IEnumerable<Assignment>> GetAssignmentsByClass(int classId);
    }

}
