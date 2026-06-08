using FluentValidation;
using School_Management_System.Application.DTOs.Attendance;
using School_Management_System.Domain.Enums;

public class MarkAttendanceDTOValidator : AbstractValidator<CreateAttendanceDTO>
{
    public MarkAttendanceDTOValidator()
    {
        RuleFor(x => x.StudentId).GreaterThan(0);
        RuleFor(x => x.ClassId).GreaterThan(0);

        RuleFor(x => x.Status)
          .Must(s => s == AttendanceStatus.Present
            || s == AttendanceStatus.Absent
            || s == AttendanceStatus.Late)
          .WithMessage("Invalid attendance status");



        RuleFor(x => x.Date)
            .NotEmpty();
    }
}
