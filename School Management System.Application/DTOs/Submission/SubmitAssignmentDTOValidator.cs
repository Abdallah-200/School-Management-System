using FluentValidation;
using School_Management_System.Application.DTOs.Submission;

public class SubmitAssignmentDTOValidator : AbstractValidator<CreateSubmissionDTO>
{
    public SubmitAssignmentDTOValidator()
    {
        RuleFor(x => x.AssignmentId).GreaterThan(0);
        RuleFor(x => x.FileUrl).NotEmpty();
    }
}
