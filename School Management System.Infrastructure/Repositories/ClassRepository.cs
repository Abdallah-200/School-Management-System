using School_Management_System.Domain.Entities;
using School_Management_System.Infrastructure.Data;
using School_Management_System.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

public class ClassRepository : Repository<Class>, IClassRepository
{
    private readonly SchoolContext _context;

    public ClassRepository(SchoolContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Class>> GetClassesByCourseIdAsync(int courseId)
    {
        return await _context.Classes
            .Where(c => c.CourseId == courseId)
            .ToListAsync();
    }
}
