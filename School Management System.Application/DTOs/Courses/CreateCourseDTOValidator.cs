using FluentValidation;
using School_Management_System.Application.DTOs.Courses;

public class CreateCourseDTOValidator : AbstractValidator<CreateCourseDTO>
{
    public CreateCourseDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.Code)
            .NotEmpty().MaximumLength(20);

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0);

        RuleFor(x => x.Credits)
            .InclusiveBetween(1, 6);
    }
}
