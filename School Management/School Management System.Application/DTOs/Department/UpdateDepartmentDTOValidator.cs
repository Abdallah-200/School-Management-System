using FluentValidation;
using School_Management_System.Application.DTOs.Department;

public class UpdateDepartmentDTOValidator : AbstractValidator<UpdateDepartmentDTO>
{
    public UpdateDepartmentDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(300);
    }
}
