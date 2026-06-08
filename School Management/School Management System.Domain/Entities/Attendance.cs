using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Enums; 

namespace School_Management_System.Domain.Entities
{ 
public class Attendance
{
    public int Id { get; set; }
    public int ClassId { get; set; }
    public Class Class { get; set; }

    public int StudentId { get; set; }
    public User Student { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public AttendanceStatus Status { get; set; } 

    public int MarkedByTeacherId { get; set; }
    public User MarkedByTeacher { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}


}