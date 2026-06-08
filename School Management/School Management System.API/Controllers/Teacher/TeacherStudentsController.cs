using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Enums;

namespace School_Management_System.API.Controllers.Teacher
{
    [ApiController]
    [Route("api/teacher/students")]
    [Authorize(Roles = "Teacher")]
    public class TeacherStudentsController : ControllerBase
    {
        private readonly IUserService _userService;

        public TeacherStudentsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users.Where(u => u.Role == UserRole.Student));
        }
    }
}
