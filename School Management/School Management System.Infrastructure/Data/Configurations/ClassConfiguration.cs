using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School_Management_System.Domain.Entities;

namespace School_Management_System.Infrastructure.Configurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Classes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Semester)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.Course)
                .WithMany(c => c.Classes)
                .HasForeignKey(x => x.CourseId);

            builder.HasOne(x => x.Teacher)
                .WithMany()
                .HasForeignKey(x => x.TeacherId);

            builder.HasMany(x => x.StudentClasses)
                .WithOne(sc => sc.Class)
                .HasForeignKey(sc => sc.ClassId);
        }
    }
}
