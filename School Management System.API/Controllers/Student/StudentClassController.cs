using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.Interfaces;
using System.Security.Claims;

namespace School_Management_System.API.Controllers.Student
{
    [ApiController]
    [Route("api/student/classes")]
    [Authorize(Roles = "Student")]
    public class StudentClassController : ControllerBase
    {
        private readonly IStudentClassService _service;

        public StudentClassController(IStudentClassService service)
        {
            _service = service;
        }

   
        [HttpGet]
        public async Task<IActionResult> GetMyClasses()
        {
            
            int studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var list = await _service.GetStudentClasses(studentId);

            return Ok(list);
        }
    }
}
