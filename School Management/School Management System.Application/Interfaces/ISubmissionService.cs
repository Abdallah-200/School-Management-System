using School_Management_System.Application.DTOs.Submission;
using School_Management_System.Domain.Entities;

namespace School_Management_System.Application.Interfaces
{
    public interface ISubmissionService
    {
        // ✓ FIX: studentId & teacherId now passed from controller (extracted from JWT)
        Task<Submission>            SubmitAssignmentAsync(CreateSubmissionDTO dto, int studentId);
        Task<Submission>            GradeSubmissionAsync(GradeSubmissionDTO dto, int teacherId);
        Task<IEnumerable<Submission>> GetSubmissionsByAssignment(int assignmentId);
        Task<IEnumerable<Submission>> GetSubmissionsByStudent(int studentId);
    }
}
