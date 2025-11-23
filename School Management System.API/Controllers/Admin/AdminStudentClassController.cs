using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.StudentClass;
using School_Management_System.Application.Interfaces;

namespace School_Management_System.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/student-classes")]
    [Authorize(Roles = "Admin,Teacher")]
    public class AdminStudentClassController : ControllerBase
    {
        private readonly IStudentClassService _studentClassService;

        public AdminStudentClassController(IStudentClassService studentClassService)
        {
            _studentClassService = studentClassService;
        }

        // Enroll a student to a class
        [HttpPost]
        public async Task<IActionResult> EnrollStudent(EnrollStudentDTO dto)
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

        // Get all classes for a student
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentClasses(int studentId)
        {
            var enrollments = await _studentClassService.GetStudentClasses(studentId);
            return Ok(enrollments);
        }

        // Optional: Get all students in a class
        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetClassStudents(int classId)
        {
            var enrollments = await _studentClassService.GetClassStudents(classId);
            return Ok(enrollments);
        }
    }
}
