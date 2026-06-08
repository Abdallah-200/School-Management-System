using Microsoft.EntityFrameworkCore;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;


namespace School_Management_System.Infrastructure.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly SchoolContext schoolContext;

        public CourseRepository(SchoolContext schoolContext) : base(schoolContext) 
        {
            this.schoolContext = schoolContext;
        }
        public async Task<IEnumerable<Course>> GetCoursesByDepartmentIdAsync(int departmentId)
        {
            return await schoolContext.Courses
                .Where(c => c.DepartmentId == departmentId)
                .ToListAsync();
        }

    }
}
