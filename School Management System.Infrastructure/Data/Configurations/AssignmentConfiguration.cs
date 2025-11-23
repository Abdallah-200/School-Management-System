using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management_System.Domain.Entities;

namespace School_Management_System.Infrastructure.Configurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("Assignments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(a => a.Description)
                   .HasMaxLength(1000);

            builder.HasOne(a => a.Class)
                   .WithMany(c => c.Assignments)
                   .HasForeignKey(a => a.ClassId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.CreatedByTeacher)
                   .WithMany()
                   .HasForeignKey(a => a.CreatedByTeacherId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
