using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.Interfaces;
using System.Security.Claims;

[ApiController]
[Route("api/student/attendance")]
[Authorize(Roles = "Student")]
public class StudentAttendanceController : ControllerBase
{
    private readonly IStudentAttendanceService _service;

    public StudentAttendanceController(IStudentAttendanceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyAttendance()
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int studentId))
            return Unauthorized();

        var attendance = await _service.GetStudentAttendanceAsync(studentId);

        return Ok(attendance);
    }
}
