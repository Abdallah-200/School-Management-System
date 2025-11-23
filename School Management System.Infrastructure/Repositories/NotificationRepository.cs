using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Enums;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace School_Management_System.Infrastructure.Repositories
{

    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly SchoolContext _context;

        public NotificationRepository(SchoolContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetByRecipientRoleAsync(UserRole role)
        {
            return await _context.Notifications
                                 .Where(n => n.RecipientRole == role && n.RecipientId == null)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetByRecipientIdAsync(int userId)
        {
            return await _context.Notifications
                                 .Where(n => n.RecipientId == userId)
                                 .ToListAsync();
        }
    }
}
