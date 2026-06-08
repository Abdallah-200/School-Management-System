using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management_System.Application.DTOs.Notification;
using School_Management_System.Application.Interfaces;

namespace School_Management_System.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/notifications")]
    [Authorize(Roles = "Admin")]
    public class AdminNotificationsController : ControllerBase
    {
        private readonly INotificationService _service;

        public AdminNotificationsController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
    }

}
