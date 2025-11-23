

namespace School_Management_System.Application.DTOs.Submission
{
    public class CreateSubmissionDTO
    {
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public string FileUrl { get; set; }
    }
}
