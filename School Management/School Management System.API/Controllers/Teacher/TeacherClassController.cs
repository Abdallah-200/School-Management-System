using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.Class;
using School_Management_System.Application.Interfaces;

namespace School_Management_System.API.Controllers.Teacher
{
    [ApiController]
    [Route("api/teacher/classes")]
    [Authorize(Roles = "Teacher")]
    public class TeacherClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public TeacherClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassDTO dto)
        {
            var result = await _classService.CreateClassAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var classes = await _classService.GetAllAsync();
            return Ok(classes);
        }
    }

}
