namespace School_Management_System.Domain.Entities
{
    public class Assignment
    {
        public int Id { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }
 
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int CreatedByTeacherId { get; set; }
        public User CreatedByTeacher { get; set; }

        public ICollection<Submission> Submissions { get; set; }
    }
}
