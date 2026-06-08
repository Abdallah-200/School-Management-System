using School_Management_System.Domain.Enums;


namespace School_Management_System.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public UserRole RecipientRole { get; set; }

        public int? RecipientId { get; set; }
        public User? Recipient { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; } = false;
    }
}
