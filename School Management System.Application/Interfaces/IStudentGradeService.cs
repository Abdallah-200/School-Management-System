using School_Management_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Interfaces
{
    public interface IStudentGradeService
    {
        Task<IEnumerable<Grade>> GetGradesForStudent(int studentId);
    }

}
