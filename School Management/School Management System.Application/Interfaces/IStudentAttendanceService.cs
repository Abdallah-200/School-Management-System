using School_Management_System.Domain.Entities;


namespace School_Management_System.Application.Interfaces
{
    public interface IStudentAttendanceService
    {
        Task<IEnumerable<Attendance>> GetStudentAttendanceAsync(int studentId);
    }

}
