using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management_System.Domain.Entities;

namespace School_Management_System.Infrastructure.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendances");

            builder.HasKey(a => a.Id);

            
            builder.HasOne(a => a.Class)
                   .WithMany()
                   .HasForeignKey(a => a.ClassId)
                   .OnDelete(DeleteBehavior.Cascade); 


            builder.HasOne(a => a.Student)
                   .WithMany()
                   .HasForeignKey(a => a.StudentId)
                   .OnDelete(DeleteBehavior.Restrict); 

         

            builder.HasOne(a => a.MarkedByTeacher)
                   .WithMany()
                   .HasForeignKey(a => a.MarkedByTeacherId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
