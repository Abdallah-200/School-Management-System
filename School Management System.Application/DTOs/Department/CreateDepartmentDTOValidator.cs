using FluentValidation;
using School_Management_System.Application.DTOs.Department;

public class CreateDepartmentDTOValidator : AbstractValidator<CreateDepartmentDTO>
{
    public CreateDepartmentDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(300);

        RuleFor(x => x.HeadOfDepartmentId)
            .GreaterThan(0).WithMessage("Invalid HeadOfDepartmentId");
    }
}
