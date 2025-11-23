using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace School_Management_System.Infrastructure.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        private readonly SchoolContext _context;

        public AssignmentRepository(SchoolContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsForStudent(int studentId)
        {
            
            var classIds = await _context.StudentClasses
                                 .Where(sc => sc.StudentId == studentId) 
                                 .Select(sc => sc.ClassId)
                                 .ToListAsync();

            var assignments = await _context.Assignments
                                 .Where(a => classIds.Contains(a.ClassId))
                                 .ToListAsync();

            return assignments;
        }


        public async Task<IEnumerable<Assignment>> GetByClassIdAsync(int classId)
        {
            return await _context.Assignments
                                 .Where(a => a.ClassId == classId)
                                 .ToListAsync();
        }
    }

}
