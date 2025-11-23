// Application/Services/AuthService.cs
using BCrypt.Net;
using School_Management_System.Application.DTOs.Auth;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepo; // generic repo or IUserRepository
    private readonly IJwtService _jwt;

    public AuthService(IRepository<User> userRepo, IJwtService jwt)
    {
        _userRepo = userRepo;
        _jwt = jwt;
    }

    public async Task<AuthResultDTO> RegisterAsync(RegisterDTO dto)
    {
        var users = await _userRepo.GetAllAsync();
        if (users.Any(u => u.Email.ToLower() == dto.Email.ToLower()))
            throw new Exception("Email already exists");

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role,
            CreatedDate = DateTime.UtcNow
        };

        await _userRepo.AddAsync(user);

        
        var token = _jwt.GenerateToken(user);
        var refresh = _jwt.GenerateRefreshToken();
        user.RefreshToken = refresh;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7); // refresh valid for 7 days
        _userRepo.Update(user);

        return new AuthResultDTO { Token = token, RefreshToken = refresh, ExpiresAt = _jwt.GetExpiry() };
    }

    public async Task<AuthResultDTO?> LoginAsync(LoginDTO dto)
    {
        var users = await _userRepo.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Email.ToLower() == dto.Email.ToLower());
        if (user == null) return null;

        var valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        if (!valid) return null;

        var token = _jwt.GenerateToken(user);
        var refresh = _jwt.GenerateRefreshToken();
        user.RefreshToken = refresh;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        _userRepo.Update(user);

        return new AuthResultDTO { Token = token, RefreshToken = refresh, ExpiresAt = _jwt.GetExpiry() };
    }

    public async Task<AuthResultDTO?> RefreshTokenAsync(string refreshToken)
    {
        var users = await _userRepo.GetAllAsync();
        var user = users.FirstOrDefault(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow);
        if (user == null) return null;

        var token = _jwt.GenerateToken(user);
        var newRefresh = _jwt.GenerateRefreshToken();
        user.RefreshToken = newRefresh;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        _userRepo.Update(user);

        return new AuthResultDTO { Token = token, RefreshToken = newRefresh, ExpiresAt = _jwt.GetExpiry() };
    }
}
