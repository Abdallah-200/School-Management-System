namespace School_Management_System.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DepartmentId { get; set; }

        public int Credits { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        // Relationships
        public Department Department { get; set; }
        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
