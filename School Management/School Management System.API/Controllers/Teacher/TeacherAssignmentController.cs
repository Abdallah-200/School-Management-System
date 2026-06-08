using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.Assignment;
using School_Management_System.Application.Interfaces;
using System.Security.Claims;

namespace School_Management_System.API.Controllers.Teacher
{
    [ApiController]
    [Route("api/teacher/assignments")]
    [Authorize(Roles = "Teacher")]
    public class TeacherAssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public TeacherAssignmentController(IAssignmentService assignmentService)
            => _assignmentService = assignmentService;

        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentDTO dto)
        {
            // ✓ FIX: Extract teacher identity from JWT — client can no longer impersonate others
            dto.CreatedByTeacherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _assignmentService.CreateAssignmentAsync(dto);
            return CreatedAtAction(nameof(GetAssignmentsByClass),
                new { classId = result.ClassId }, result);
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetAssignmentsByClass(int classId)
        {
            var assignments = await _assignmentService.GetAssignmentsByClass(classId);
            return Ok(assignments);
        }
    }
}
