namespace School_Management_System.Application.DTOs.Assignment
{
    public class CreateAssignmentDTO
    {
        public int ClassId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int CreatedByTeacherId { get; set; }
    }
}
