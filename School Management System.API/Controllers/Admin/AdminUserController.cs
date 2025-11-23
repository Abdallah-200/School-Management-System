using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.User;
using School_Management_System.Application.Interfaces;

namespace School_Management_System.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/users")]
    [Authorize(Roles = "Admin")]
    public class AdminUserController : ControllerBase
    {
        
       
            private readonly IUserService _userService;
            public AdminUserController(IUserService userService)
            {
                _userService = userService;
            }

            [HttpPost]
            public async Task<IActionResult> CreateUser(CreateUserDTO dto)
            {
                var user = await _userService.CreateUserAsync(dto);
                return Ok(user);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetUser(int id)
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null) return NotFound();
                return Ok(user);
            }

            [HttpGet]
            public async Task<IActionResult> GetAllUsers()
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserDTO dto)
        {
            var updated = await _userService.UpdateUserAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
