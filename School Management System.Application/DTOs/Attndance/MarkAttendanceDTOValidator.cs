using FluentValidation;
using School_Management_System.Application.DTOs.Attendance;

public class MarkAttendanceDTOValidator : AbstractValidator<CreateAttendanceDTO>
{
    public MarkAttendanceDTOValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(0);
        RuleFor(x => x.ClassId).GreaterThan(0);

        RuleFor(x => x.Status)
            .Must(s => s == "Present" || s == "Absent" || s == "Late")
            .WithMessage("Invalid attendance status");

        RuleFor(x => x.Date)
            .NotEmpty();
    }
}
