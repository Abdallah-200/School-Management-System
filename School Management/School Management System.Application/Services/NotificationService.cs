using AutoMapper;
using School_Management_System.Application.DTOs.Notification;
using School_Management_System.Application.Interfaces;
using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Enums;
using School_Management_System.Domain.Interfaces;

namespace School_Management_System.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;
        private readonly IMapper                 _mapper;

        public NotificationService(INotificationRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<Notification> CreateAsync(CreateNotificationDTO dto)
        {
            var notification = _mapper.Map<Notification>(dto);
            await _repo.AddAsync(notification);
            await _repo.SaveChangesAsync(); // ✓ FIX
            return notification;
        }

        public async Task<IEnumerable<Notification>> GetForUserAsync(int userId, UserRole role)
        {
            var direct = await _repo.GetByRecipientIdAsync(userId);
            var byRole = await _repo.GetByRecipientRoleAsync(role);
            return direct.Concat(byRole).DistinctBy(n => n.Id);
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var n = await _repo.GetByIdAsync(notificationId);
            if (n == null) return;
            n.IsRead = true;
            _repo.Update(n);
            await _repo.SaveChangesAsync(); // ✓ FIX
        }
    }
}
