using BCrypt.Net;
using School_Management_System.Application.DTOs.Auth;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Enums;
using School_Management_System.Domain.Interfaces;

namespace School_Management_System.Application.Services
{
    public class AuthService : IAuthService
    {
        // ✓ FIX: Now uses IUserRepository (not IRepository<User>) for efficient DB queries
        private readonly IUserRepository _userRepo;
        private readonly IJwtService     _jwt;

        public AuthService(IUserRepository userRepo, IJwtService jwt)
        {
            _userRepo = userRepo;
            _jwt      = jwt;
        }

        public async Task<AuthResultDTO> RegisterAsync(RegisterDTO dto)
        {
            // ✓ FIX: DB-level lookup by email — no more loading all users into memory
            var existing = await _userRepo.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new InvalidOperationException("Email already registered.");

            var user = new User
            {
                FullName     = dto.FullName,
                Email        = dto.Email.ToLower().Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                       
                // ✓ FIX: Role always defaults to Student — not client-controlled
                Role         = UserRole.Student,
                CreatedDate  = DateTime.UtcNow,
                IsActive     = true
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync(); // ✓ FIX: Was never called before

            var (token, refresh) = IssueTokens(user);
            await _userRepo.SaveChangesAsync();

            return new AuthResultDTO { 
                Token = token,
                RefreshToken = refresh,
                ExpiresAt = _jwt.GetExpiry(),
                UserId = user.Id,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResultDTO?> LoginAsync(LoginDTO dto)
        {
            // ✓ FIX: Single DB query instead of loading all users
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null || !user.IsActive) return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return null;

            var (token, refresh) = IssueTokens(user);
            await _userRepo.SaveChangesAsync(); // ✓ FIX: SaveChanges for token update

            return new AuthResultDTO { 
                Token = token,
                RefreshToken = refresh,
                ExpiresAt = _jwt.GetExpiry(),
                UserId = user.Id,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResultDTO?> RefreshTokenAsync(string refreshToken)
        {
            // ✓ FIX: DB-level lookup for refresh token (validates expiry in DB query)
            var user = await _userRepo.GetByRefreshTokenAsync(refreshToken);
            if (user == null) return null;

            var (token, refresh) = IssueTokens(user);
            await _userRepo.SaveChangesAsync(); // ✓ FIX: SaveChanges for new token

            return new AuthResultDTO {
                Token = token,
                RefreshToken = refresh,
                ExpiresAt = _jwt.GetExpiry(),
                UserId = user.Id,
                Role = user.Role.ToString()
            };
        }

        // ─── private helper ──────────────────────────────────────────────
        private (string token, string refresh) IssueTokens(User user)
        {
            var token   = _jwt.GenerateToken(user);
            var refresh = _jwt.GenerateRefreshToken();
            user.RefreshToken       = refresh;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            _userRepo.Update(user);
            return (token, refresh);
        }
    }
}
