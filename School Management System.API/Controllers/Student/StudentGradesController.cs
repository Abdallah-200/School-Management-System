using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.Interfaces;
using System.Security.Claims;

namespace School_Management_System.API.Controllers.Student
{
    [Route("api/student/grades")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentGradesController : ControllerBase
    {
        private readonly IStudentGradeService _gradeService;

        public StudentGradesController(IStudentGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        // GET: api/student/grades
        [HttpGet]
        public async Task<IActionResult> GetMyGrades()
        {
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("Invalid token");

            int studentId = int.Parse(userIdClaim);

            var grades = await _gradeService.GetGradesForStudent(studentId);

            if (!grades.Any())
                return Ok(new { message = "No grades found for this student." });

            return Ok(grades);
        }
    }
}
