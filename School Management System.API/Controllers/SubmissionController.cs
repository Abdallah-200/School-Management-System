using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.Submission;
using School_Management_System.Application.Interfaces;

namespace School_Management_System.API.Controllers
{
    [ApiController]
    [Route("api/submissions")]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        // Student submits assignment
        [HttpPost("submit")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> SubmitAssignment(CreateSubmissionDTO dto)
        {
            var result = await _submissionService.SubmitAssignmentAsync(dto);
            return Ok(result);
        }

        // Teacher grades submission
        [HttpPost("grade")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GradeSubmission(GradeSubmissionDTO dto)
        {
            var result = await _submissionService.GradeSubmissionAsync(dto);
            return Ok(result);
        }

        // Get submissions for an assignment
        [HttpGet("assignment/{assignmentId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetSubmissionsByAssignment(int assignmentId)
        {
            var submissions = await _submissionService.GetSubmissionsByAssignment(assignmentId);
            return Ok(submissions);
        }

        // Get submissions by student
        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Teacher")]
        public async Task<IActionResult> GetSubmissionsByStudent(int studentId)
        {
            var submissions = await _submissionService.GetSubmissionsByStudent(studentId);
            return Ok(submissions);
        }
    }

}
