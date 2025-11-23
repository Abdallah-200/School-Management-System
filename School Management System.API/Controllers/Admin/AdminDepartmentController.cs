using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.Department;
using School_Management_System.Application.Interfaces;

namespace School_Management_System.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/departments")]
    [Authorize(Roles = "Admin")]
    public class AdminDepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public AdminDepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDTO dto)
        {
            var result = await _departmentService.CreateDepartmentAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _departmentService.GetAllDepartmentsAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dep = await _departmentService.GetDepartmentByIdAsync(id);
            if (dep == null) return NotFound();
            return Ok(dep);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDepartmentDTO dto)
        {
            var updated = await _departmentService.UpdateDepartmentAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _departmentService.DeleteDepartmentAsync(id);
            if (!success)
                return NotFound();

            return NoContent(); 
        }
    }
}
