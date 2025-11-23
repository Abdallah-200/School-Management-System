using AutoMapper;
using School_Management_System.Application.DTOs.Notification;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Enums;
using School_Management_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;
        private readonly IMapper mapper;

        public NotificationService(INotificationRepository repo , IMapper mapper)
        {
            _repo = repo;
            this.mapper = mapper;
        }

        public async Task<Notification> CreateAsync(CreateNotificationDTO dto)
        {
            var notification = mapper.Map<Notification>(dto);
            
            await _repo.AddAsync(notification);
            return notification;
        }

        public async Task<IEnumerable<Notification>> GetForUserAsync(int userId, UserRole role)
        {
            var direct = await _repo.GetByRecipientIdAsync(userId);
            var byRole = await _repo.GetByRecipientRoleAsync(role);

            return direct.Concat(byRole);
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var n = await _repo.GetByIdAsync(notificationId);
            if (n == null) return;

            n.IsRead = true;
            _repo.Update(n);
        }
    }

}
