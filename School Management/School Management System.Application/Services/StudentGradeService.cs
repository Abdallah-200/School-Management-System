using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

public class StudentGradeService : IStudentGradeService
{
    private readonly IGradeRepository _repo;

    public StudentGradeService(IGradeRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Grade>> GetGradesForStudent(int studentId)
    {
        return await _repo.GetGradesForStudent(studentId);
    }
}
