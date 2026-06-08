namespace School_Management_System.Domain.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int TeacherId { get; set; }
        public User Teacher { get; set; }

        public string Semester { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        // A class contains multiple students (mapping table)
        public ICollection<StudentClass> StudentClasses { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

    }
}
