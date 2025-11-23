using School_Management_System.Application.DTOs.Submission;
using School_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Interfaces
{
    public interface ISubmissionService
    {
        Task<Submission> SubmitAssignmentAsync(CreateSubmissionDTO dto);
        Task<Submission> GradeSubmissionAsync(GradeSubmissionDTO dto);
        Task<IEnumerable<Submission>> GetSubmissionsByAssignment(int assignmentId);
        Task<IEnumerable<Submission>> GetSubmissionsByStudent(int studentId);
    }

}
