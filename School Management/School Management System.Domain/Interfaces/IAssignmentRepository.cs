using School_Management_System.Domain.Entities;


namespace School_Management_System.Domain.Interfaces
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        Task<IEnumerable<Assignment>> GetByClassIdAsync(int classId);
        Task<IEnumerable<Assignment>> GetAssignmentsForStudent(int userId);

    }

}
