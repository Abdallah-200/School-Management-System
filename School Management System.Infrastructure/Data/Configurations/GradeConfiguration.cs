using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management_System.Domain.Entities;


namespace School_Management_System.Infrastructure.Data.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grade");

            builder.HasKey(g => g.Id);

            builder.HasOne(g => g.Class)
                   .WithMany()
                   .HasForeignKey(g => g.ClassId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Assignment)
                   .WithMany()
                   .HasForeignKey(g => g.AssignmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.StudentClass)
                   .WithMany()
                   .HasForeignKey(g => g.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
