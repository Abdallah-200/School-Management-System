using School_Management_System.Application.DTOs.Attendance;
using School_Management_System.Domain.Entities;


namespace School_Management_System.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<Attendance> MarkAttendanceAsync(CreateAttendanceDTO dto);
        Task<IEnumerable<Attendance>> GetClassAttendance(int classId);
        Task<IEnumerable<Attendance>> GetStudentAttendance(int studentId);
    }

}
