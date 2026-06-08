using AutoMapper;
using School_Management_System.Application.DTOs.Submission;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

namespace School_Management_System.Application.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _repo;
        private readonly IMapper               _mapper;

        public SubmissionService(ISubmissionRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Submission> SubmitAssignmentAsync(CreateSubmissionDTO dto, int studentId)
        {
            var submission = _mapper.Map<Submission>(dto);
            // ✓ FIX: StudentId comes from JWT, not client body
            submission.StudentId     = studentId;
            submission.SubmittedDate = DateTime.UtcNow;
            await _repo.AddAsync(submission);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return submission;
        }

        public async Task<Submission> GradeSubmissionAsync(GradeSubmissionDTO dto, int teacherId)
        {
            var submission = await _repo.GetByIdAsync(dto.SubmissionId)
                ?? throw new KeyNotFoundException($"Submission {dto.SubmissionId} not found.");

            submission.Grade              = dto.Grade;
            // ✓ FIX: TeacherId comes from JWT, not client body
            submission.GradedByTeacherId  = teacherId;
            submission.Remarks            = dto.Remarks;
            _repo.Update(submission);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return submission;
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByAssignment(int assignmentId)
            => await _repo.GetByAssignmentIdAsync(assignmentId);

        public async Task<IEnumerable<Submission>> GetSubmissionsByStudent(int studentId)
            => await _repo.GetByStudentIdAsync(studentId);
    }
}
