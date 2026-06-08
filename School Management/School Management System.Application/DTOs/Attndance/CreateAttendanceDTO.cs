using School_Management_System.Domain.Enums;

namespace School_Management_System.Application.DTOs.Attendance
{
    public class CreateAttendanceDTO
    {
        public int              ClassId   { get; set; }
        public int              StudentId { get; set; }
        public DateTime         Date      { get; set; }
        // ✓ FIX: Was string? with Enum.Parse — now strongly typed, fails at model binding not runtime
        public AttendanceStatus Status    { get; set; }
        // ✓ FIX: MarkedByTeacherId removed — extracted from JWT in the controller
    }
}
