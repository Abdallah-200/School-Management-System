

using School_Management_System.Domain.Enums;

namespace School_Management_System.Application.DTOs.Auth
{
    public class RegisterDTO
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}
