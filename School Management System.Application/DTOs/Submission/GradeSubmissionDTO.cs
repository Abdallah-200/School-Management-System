

namespace School_Management_System.Application.DTOs.Submission
{
    public class GradeSubmissionDTO
    {
        public int SubmissionId { get; set; }
        public decimal Grade { get; set; }
        public int GradedByTeacherId { get; set; }
        public string Remarks { get; set; }
    }
}
