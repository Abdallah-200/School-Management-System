namespace School_Management_System.Application.DTOs.StudentClass
{
    public class StudentClassDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
