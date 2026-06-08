namespace School_Management_System.Application.DTOs.Submission
{
    public class GradeSubmissionDTO
    {
        public int     SubmissionId { get; set; }
        public decimal Grade        { get; set; }
        public string  Remarks      { get; set; } = null!;
        // ✓ FIX: GradedByTeacherId removed — extracted from JWT in the controller
    }
}
