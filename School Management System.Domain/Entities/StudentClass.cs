namespace School_Management_System.Domain.Entities
{
    public class StudentClass
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}
