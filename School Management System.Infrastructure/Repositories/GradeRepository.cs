using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace School_Management_System.Infrastructure.Repositories
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        private readonly SchoolContext _context;

        public GradeRepository(SchoolContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grade>> GetGradesForStudent(int studentId)
        {
            var classIds = await _context.StudentClasses
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.ClassId)
                .ToListAsync();

            return await _context.Grades
                .Where(g => g.StudentId == studentId && classIds.Contains(g.ClassId))
                .ToListAsync();
        }
    }

}
