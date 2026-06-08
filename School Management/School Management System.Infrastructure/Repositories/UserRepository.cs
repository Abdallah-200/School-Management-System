using Microsoft.EntityFrameworkCore;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;

namespace School_Management_System.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SchoolContext context) : base(context) { }

        // ✓ FIX: O(1) DB-level lookup instead of loading ALL users into memory
        public async Task<User?> GetByEmailAsync(string email)
            => await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email.ToLower());

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
            => await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.RefreshToken == refreshToken &&
                    u.RefreshTokenExpiry > DateTime.UtcNow);
    }
}
