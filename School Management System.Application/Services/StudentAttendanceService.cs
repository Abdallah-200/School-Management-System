using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Services
{
    using School_Management_System.Application.Interfaces;
    using School_Management_System.Domain.Entities;
    using School_Management_System.Domain.Interfaces;

    public class StudentAttendanceService : IStudentAttendanceService
    {
        private readonly IAttendanceRepository _repo;

        public StudentAttendanceService(IAttendanceRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Attendance>> GetStudentAttendanceAsync(int studentId)
        {
            return await _repo.GetByStudentIdAsync(studentId);
        }
    }

}
