using Microsoft.EntityFrameworkCore;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
using School_Management_System.Infrastructure.Repositories;

public class ClassRepository : Repository<Class>, IClassRepository
{
    private readonly SchoolContext _context;

    public ClassRepository(SchoolContext context) : base(context)
    {
        _context = context;
    }

    public new async Task<IEnumerable<Class>> GetAllAsync()
    {
        return await _context.Classes
            .Include(c => c.Teacher)
            .ToListAsync();
    }

    public async Task<IEnumerable<Class>> GetClassesByCourseIdAsync(int courseId)
    {
        return await _context.Classes
            .Include(c => c.Course)
            .Include(c => c.Teacher)
            .Where(c => c.CourseId == courseId)
            .ToListAsync();
    }
}


   





