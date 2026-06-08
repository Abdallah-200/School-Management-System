using School_Management_System.Domain.Entities;


namespace School_Management_System.Domain.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        Task<IEnumerable<Attendance>> GetByClassIdAsync(int classId);
        Task<IEnumerable<Attendance>> GetByStudentIdAsync(int studentId);
    }

}
