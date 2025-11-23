using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace School_Management_System.Infrastructure.Repositories
{
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        private readonly SchoolContext _context;

        public AttendanceRepository(SchoolContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetByClassIdAsync(int classId)
        {
            return await _context.Attendances
                                 .Where(a => a.ClassId == classId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Attendances
                                 .Where(a => a.StudentId == studentId)
                                 .ToListAsync();
        }
    }

}
