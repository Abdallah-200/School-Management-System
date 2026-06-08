using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Domain.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetByRecipientRoleAsync(UserRole role);
        Task<IEnumerable<Notification>> GetByRecipientIdAsync(int userId);
    }

}
