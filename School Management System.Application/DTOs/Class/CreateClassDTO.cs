namespace School_Management_System.Application.DTOs.Class
{
    public class CreateClassDTO
    {
        public string? Name { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string? Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
