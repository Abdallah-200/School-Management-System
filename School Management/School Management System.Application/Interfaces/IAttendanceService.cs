using School_Management_System.Application.DTOs.Attendance;
using School_Management_System.Domain.Entities;

namespace School_Management_System.Application.Interfaces
{
    public interface IAttendanceService
    {
        // ✓ FIX: teacherId passed from controller (extracted from JWT)
        Task<Attendance>            MarkAttendanceAsync(CreateAttendanceDTO dto, int teacherId);
        Task<IEnumerable<Attendance>> GetClassAttendance(int classId);
        Task<IEnumerable<Attendance>> GetStudentAttendance(int studentId);
    }
}
