using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.Interfaces;

namespace School_Management_System.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/classes")]
    [Authorize(Roles = "Admin")]
    public class AdminClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public AdminClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var classes = await _classService.GetAllAsync();
            return Ok(classes);
        }
    }
}
