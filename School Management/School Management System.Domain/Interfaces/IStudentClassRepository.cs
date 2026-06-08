using School_Management_System.Domain.Entities;


namespace School_Management_System.Domain.Interfaces
{
    public interface IStudentClassRepository : IRepository<StudentClass>
    {
        Task<IEnumerable<StudentClass>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<StudentClass>> GetByClassIdAsync(int classId);
    }

}
