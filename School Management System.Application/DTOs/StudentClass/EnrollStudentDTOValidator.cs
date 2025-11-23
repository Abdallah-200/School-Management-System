using FluentValidation;
using School_Management_System.Application.DTOs.StudentClass;

public class EnrollStudentDTOValidator : AbstractValidator<EnrollStudentDTO>
{
    public EnrollStudentDTOValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(0);
        RuleFor(x => x.ClassId).GreaterThan(0);
    }
}
