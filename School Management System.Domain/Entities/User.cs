using School_Management_System.Domain.Enums;

namespace School_Management_System.Domain.Entities
{
    
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

    
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }


        public ICollection<StudentClass>? StudentClasses { get; set; }
        public ICollection<Class>? Classes { get; set; }
    }

}
