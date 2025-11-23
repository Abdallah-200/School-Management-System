using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.Attendance;
using School_Management_System.Application.Interfaces;

namespace School_Management_System.API.Controllers.Teacher
{
    [ApiController]
    [Route("api/teacher/attendance")]
    [Authorize(Roles = "Teacher")]
    public class TeacherAttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public TeacherAttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        public async Task<IActionResult> MarkAttendance(CreateAttendanceDTO dto)
        {
            var result = await _attendanceService.MarkAttendanceAsync(dto);
            return Ok(result);
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetClassAttendance(int classId)
        {
            var attendances = await _attendanceService.GetClassAttendance(classId);
            return Ok(attendances);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentAttendance(int studentId)
        {
            var attendances = await _attendanceService.GetStudentAttendance(studentId);
            return Ok(attendances);
        }
    }

}
