using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.StudentClass;
using School_Management_System.Application.Interfaces;

namespace School_Management_System.API.Controllers.Teacher
{
    [ApiController]
    [Route("api/teacher/student-classes")]
    [Authorize(Roles = "Teacher")]
    public class TeacherStudentClassController : ControllerBase
    {
        private readonly IStudentClassService _studentClassService;

        public TeacherStudentClassController(IStudentClassService studentClassService)
        {
            _studentClassService = studentClassService;
        }

        [HttpPost]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollStudentDTO dto)
        {
            var studentClass = new Domain.Entities.StudentClass
            {
                StudentId = dto.StudentId,
                ClassId = dto.ClassId,
                EnrollmentDate = DateTime.UtcNow
            };

            var result = await _studentClassService.EnrollStudentAsync(studentClass);
            return Ok(result);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentClasses(int studentId)
        {
            var enrollments = await _studentClassService.GetStudentClasses(studentId);
            return Ok(enrollments);
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetClassStudents(int classId)
        {
            var enrollments = await _studentClassService.GetClassStudents(classId);
            return Ok(enrollments);
        }
    }
}
