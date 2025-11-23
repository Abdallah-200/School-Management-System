using School_Management_System.Application.DTOs.Auth;


namespace School_Management_System.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDTO> RegisterAsync(RegisterDTO dto);
        Task<AuthResultDTO?> LoginAsync(LoginDTO dto);
        Task<AuthResultDTO?> RefreshTokenAsync(string refreshToken);
    }
}
