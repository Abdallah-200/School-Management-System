using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

namespace School_Management_System.Application.Services
{
    public class StudentClassService : IStudentClassService
    {
        private readonly IStudentClassRepository _repo;

        public StudentClassService(IStudentClassRepository repo) => _repo = repo;

        public async Task<StudentClass> EnrollStudentAsync(StudentClass model)
        {
            await _repo.AddAsync(model);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return model;
        }

        public async Task<IEnumerable<StudentClass>> GetStudentClasses(int studentId)
            => await _repo.GetByStudentIdAsync(studentId);

        public async Task<IEnumerable<StudentClass>> GetClassStudents(int classId)
            => await _repo.GetByClassIdAsync(classId);
    }
}
