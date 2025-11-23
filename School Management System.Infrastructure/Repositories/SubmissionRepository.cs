using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace School_Management_System.Infrastructure.Repositories
{
    public class SubmissionRepository : Repository<Submission>, ISubmissionRepository
    {
        private readonly SchoolContext _context;

        public SubmissionRepository(SchoolContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Submission>> GetByAssignmentIdAsync(int assignmentId)
        {
            return await _context.Submissions
                                 .Where(s => s.AssignmentId == assignmentId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Submission>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Submissions
                                 .Where(s => s.StudentId == studentId)
                                 .ToListAsync();
        }
    }

}
