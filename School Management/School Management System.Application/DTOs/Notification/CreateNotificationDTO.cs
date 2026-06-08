using School_Management_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Application.DTOs.Notification
{
    public class CreateNotificationDTO
    {
        public required string Title { get; set; }
        public required string Message { get; set; }
        public UserRole RecipientRole { get; set; }
        public int? RecipientId { get; set; }
    }

}
