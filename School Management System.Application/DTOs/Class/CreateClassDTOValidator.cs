using FluentValidation;
using School_Management_System.Application.DTOs.Class;


public class CreateClassDTOValidator : AbstractValidator<CreateClassDTO>
{
    public CreateClassDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.CourseId).GreaterThan(0);
        RuleFor(x => x.TeacherId).GreaterThan(0);

        RuleFor(x => x.Semester)
            .NotEmpty().MaximumLength(50);

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("StartDate must be before EndDate");
    }
}
