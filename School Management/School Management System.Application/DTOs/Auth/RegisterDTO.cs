namespace School_Management_System.Application.DTOs.Auth
{
    public class RegisterDTO
    {
        public string FullName { get; set; } = null!;
        public string Email    { get; set; } = null!;
        public string Password { get; set; } = null!;
        // ✓ FIX: Role removed — self-registration always creates a Student.
        //         Admins create teachers/admins via the Admin user management endpoint.
    }
}
