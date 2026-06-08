using School_Management_System.Domain.Entities;
using School_Management_System.Domain.Enums;

namespace School_Management_System.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int HeadOfDepartmentId { get; set; }  // Teacher Only

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Navigation
        public User HeadOfDepartment { get; set; } = null!;
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
