namespace School_Management_System.Application.DTOs.StudentClass
{
    public class StudentClassDTO
    {
        public int StudentId { get; set; }
        public required string StudentName { get; set; }

        public int ClassId { get; set; }
        public required string ClassName { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
