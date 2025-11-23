namespace School_Management_System.Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }

        public int StudentId { get; set; }      
        public int ClassId { get; set; }         
        public int AssignmentId { get; set; }    

        public double Score { get; set; }        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relations
        public Assignment Assignment { get; set; }
        public Class Class { get; set; }
        public StudentClass StudentClass { get; set; }
    }
}
