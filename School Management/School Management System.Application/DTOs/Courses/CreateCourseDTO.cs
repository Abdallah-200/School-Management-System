namespace School_Management_System.Application.DTOs.Courses
{
    public class CreateCourseDTO
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int DepartmentId { get; set; }
        public int Credits { get; set; }
    }
}
