using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

public interface IClassRepository : IRepository<Class>
{
    Task<IEnumerable<Class>> GetClassesByCourseIdAsync(int courseId);
}
