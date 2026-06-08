using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace School_Management_System.API.Controllers.Student
{
    [ApiController]
    [Route("api/student/assignments")]
    [Authorize(Roles = "Student")]
    public class StudentAssignmentController : ControllerBase
    {
        private readonly IStudentAssignmentService _service;

        public StudentAssignmentController(IStudentAssignmentService service)
        {
            _service = service;
        }

        // GET: /api/student/assignments
        [HttpGet]
        public async Task<IActionResult> GetAssignments()
        {
            
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int studentId))
                return Unauthorized();

            var assignments = await _service.GetStudentAssignments(studentId);

            return Ok(assignments);
        }

        
        [HttpPost("{id}/submit")]
        public async Task<IActionResult> SubmitAssignment(int id, IFormFile file)
        {
            if (file == null)
                return BadRequest("File is required");

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int studentId))
                return Unauthorized();

            var result = await _service.SubmitAssignmentAsync(id, studentId, file);

            return Ok(result);
        }
    }
}
