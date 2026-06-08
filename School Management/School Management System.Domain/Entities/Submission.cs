

namespace School_Management_System.Domain.Entities
{
    public class Submission
    {
        public int Id { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;
        public string FileUrl { get; set; }

        public decimal? Grade { get; set; } // Nullable in case not graded yet
        public int? GradedByTeacherId { get; set; }
        public User GradedByTeacher { get; set; }

        public string Remarks { get; set; }
    }
}
