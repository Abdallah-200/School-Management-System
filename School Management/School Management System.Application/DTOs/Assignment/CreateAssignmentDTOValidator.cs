using FluentValidation;
using School_Management_System.Application.DTOs.Assignment;

public class CreateAssignmentDTOValidator : AbstractValidator<CreateAssignmentDTO>
{
    public CreateAssignmentDTOValidator()
    {
        RuleFor(x => x.ClassId).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);

        RuleFor(x => x.DueDate)
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("Due date cannot be in the past");
    }
}
