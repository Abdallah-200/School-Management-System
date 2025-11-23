using AutoMapper;
using School_Management_System.Application.DTOs.Submission;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _repo;
        private readonly IMapper mapper;

        public SubmissionService(ISubmissionRepository repo , IMapper mapper)
        {
            _repo = repo;
            this.mapper = mapper;
        }

        public async Task<Submission> SubmitAssignmentAsync(CreateSubmissionDTO dto)
        {
            var submission = mapper.Map<Submission>(dto);

            submission.SubmittedDate = DateTime.UtcNow;
         
            await _repo.AddAsync(submission);
            return submission;
        }

        public async Task<Submission> GradeSubmissionAsync(GradeSubmissionDTO dto)
        {
            var submission = await _repo.GetByIdAsync(dto.SubmissionId);
            if (submission == null) throw new Exception("Submission not found");

            submission.Grade = dto.Grade;
            submission.GradedByTeacherId = dto.GradedByTeacherId;
            submission.Remarks = dto.Remarks;

            _repo.Update(submission);

            return submission;
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByAssignment(int assignmentId)
        {
            return await _repo.GetByAssignmentIdAsync(assignmentId);
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByStudent(int studentId)
        {
            return await _repo.GetByStudentIdAsync(studentId);
        }
    }

}
