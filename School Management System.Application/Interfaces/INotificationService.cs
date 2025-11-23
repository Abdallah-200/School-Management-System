using School_Management_System.Application.DTOs.Notification;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Interfaces
{
    public interface INotificationService
    {
        Task<Notification> CreateAsync(CreateNotificationDTO dto);
        Task<IEnumerable<Notification>> GetForUserAsync(int userId, UserRole role);
        Task MarkAsReadAsync(int notificationId);
    }

}
