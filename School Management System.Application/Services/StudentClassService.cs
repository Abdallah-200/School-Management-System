using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Services
{
    public class StudentClassService : IStudentClassService
    {
        private readonly IStudentClassRepository _repo;

        public StudentClassService(IStudentClassRepository repo)
        {
            _repo = repo;
        }

        public async Task<StudentClass> EnrollStudentAsync(StudentClass model)
        {
           await _repo.AddAsync(model);
            return model;

        }

        public async Task<IEnumerable<StudentClass>> GetStudentClasses(int studentId)
        {
            return await _repo.GetByStudentIdAsync(studentId);
        }
        public async Task<IEnumerable<StudentClass>> GetClassStudents(int classId)
        {
            return await _repo.GetByClassIdAsync(classId);
        }

    }

}
