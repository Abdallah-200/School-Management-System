using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management_System.Domain.Entities;

namespace School_Management_System.Infrastructure.Configurations
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable("Submissions");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.FileUrl)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(s => s.Remarks)
                   .HasMaxLength(1000);

            builder.HasOne(s => s.Assignment)
                   .WithMany(a => a.Submissions)
                   .HasForeignKey(s => s.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Student)
                   .WithMany()
                   .HasForeignKey(s => s.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.GradedByTeacher)
                   .WithMany()
                   .HasForeignKey(s => s.GradedByTeacherId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
