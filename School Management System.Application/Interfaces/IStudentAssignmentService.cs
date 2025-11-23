using Microsoft.AspNetCore.Http;
using School_Management_System.Domain.Entities;


namespace School_Management_System.Application.Interfaces
{
    public interface IStudentAssignmentService
    {
        Task<IEnumerable<Assignment>> GetStudentAssignments(int studentId);
        Task<Submission> SubmitAssignmentAsync(int assignmentId, int studentId, IFormFile file);
        Task<IEnumerable<Submission>> GetStudentGrades(int studentId);
    }

}
