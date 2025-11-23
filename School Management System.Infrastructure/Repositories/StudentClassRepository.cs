using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace School_Management_System.Infrastructure.Repositories
{
    public class StudentClassRepository : Repository<StudentClass>, IStudentClassRepository
    {
        private readonly SchoolContext _context;

        public StudentClassRepository(SchoolContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentClass>> GetByStudentIdAsync(int studentId)
        {
            return await _context.StudentClasses
                                 .Where(sc => sc.StudentId == studentId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<StudentClass>> GetByClassIdAsync(int classId)
        {
            return await _context.StudentClasses
                                 .Where(sc => sc.ClassId == classId)
                                 .ToListAsync();
        }
    }

}
