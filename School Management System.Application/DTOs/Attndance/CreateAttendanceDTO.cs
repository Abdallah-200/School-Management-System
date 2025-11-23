namespace School_Management_System.Application.DTOs.Attendance
{
    public class CreateAttendanceDTO
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string? Status { get; set; } // "Present", "Absent", "Late"
        public int MarkedByTeacherId { get; set; }
    }
}
