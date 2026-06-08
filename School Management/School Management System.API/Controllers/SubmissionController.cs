using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.Submission;
using School_Management_System.Application.Interfaces;
using System.Security.Claims;

namespace School_Management_System.API.Controllers
{
    [ApiController]
    [Route("api/submissions")]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _service;

        public SubmissionController(ISubmissionService service)
            => _service = service;

        [HttpPost("submit")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> SubmitAssignment([FromBody] CreateSubmissionDTO dto)
        {
            // ✓ FIX: StudentId from JWT — student can only submit as themselves
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result    = await _service.SubmitAssignmentAsync(dto, studentId);
            return CreatedAtAction(nameof(GetSubmissionsByStudent),
                new { studentId }, result);
        }

        [HttpPost("grade")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GradeSubmission([FromBody] GradeSubmissionDTO dto)
        {
            // ✓ FIX: TeacherId from JWT — teacher can't grade as someone else
            var teacherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result    = await _service.GradeSubmissionAsync(dto, teacherId);
            return Ok(result);
        }

        [HttpGet("assignment/{assignmentId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetSubmissionsByAssignment(int assignmentId)
            => Ok(await _service.GetSubmissionsByAssignment(assignmentId));

        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Teacher")]
        public async Task<IActionResult> GetSubmissionsByStudent(int studentId)
        {
            // ✓ FIX: Students can only view their own submissions
            if (User.IsInRole("Student"))
                studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(await _service.GetSubmissionsByStudent(studentId));
        }
    }
}
