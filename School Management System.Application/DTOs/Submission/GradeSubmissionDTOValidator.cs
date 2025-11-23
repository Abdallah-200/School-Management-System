using FluentValidation;
using School_Management_System.Application.DTOs.Submission;

public class GradeSubmissionDTOValidator : AbstractValidator<GradeSubmissionDTO>
{
    public GradeSubmissionDTOValidator()
    {
        RuleFor(x => x.SubmissionId).GreaterThan(0);
        RuleFor(x => x.Grade)
            .InclusiveBetween(0, 100);

        RuleFor(x => x.Remarks)
            .MaximumLength(300);
    }
}
