using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management_System.Domain.Entities;

namespace School_Management_System.Infrastructure.Configurations
{
    public class StudentClassConfiguration : IEntityTypeConfiguration<StudentClass>
    {
        public void Configure(EntityTypeBuilder<StudentClass> builder)
        {
            
            builder.HasKey(sc => sc.Id);

            
            builder.HasOne(sc => sc.Student)
                   .WithMany(s => s.StudentClasses)
                   .HasForeignKey(sc => sc.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(sc => sc.Class)
                   .WithMany(c => c.StudentClasses)
                   .HasForeignKey(sc => sc.ClassId)
                   .OnDelete(DeleteBehavior.Cascade);

            
            builder.ToTable("StudentClasses");
        }
    }
}
