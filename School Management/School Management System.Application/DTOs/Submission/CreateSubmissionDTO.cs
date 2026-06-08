namespace School_Management_System.Application.DTOs.Submission
{
    public class CreateSubmissionDTO
    {
        public int    AssignmentId { get; set; }
        public string FileUrl      { get; set; } = null!;
        // ✓ FIX: StudentId removed — extracted from JWT in the controller
    }
}
