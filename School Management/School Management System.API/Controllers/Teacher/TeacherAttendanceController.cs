using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.Attendance;
using School_Management_System.Application.Interfaces;
using System.Security.Claims;

namespace School_Management_System.API.Controllers.Teacher
{
    [ApiController]
    [Route("api/teacher/attendance")]
    [Authorize(Roles = "Teacher")]
    public class TeacherAttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public TeacherAttendanceController(IAttendanceService attendanceService)
            => _attendanceService = attendanceService;

        [HttpPost]
        public async Task<IActionResult> MarkAttendance([FromBody] CreateAttendanceDTO dto)
        {
            // ✓ FIX: teacherId from JWT — teacher can't mark attendance as someone else
            var teacherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result    = await _attendanceService.MarkAttendanceAsync(dto, teacherId);
            return Ok(result);
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetClassAttendance(int classId)
            => Ok(await _attendanceService.GetClassAttendance(classId));

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentAttendance(int studentId)
            => Ok(await _attendanceService.GetStudentAttendance(studentId));
    }
}
