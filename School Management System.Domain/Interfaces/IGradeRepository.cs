using School_Management_System.Domain.Entities;


namespace School_Management_System.Domain.Interfaces
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<IEnumerable<Grade>> GetGradesForStudent(int studentId);
    }

}
