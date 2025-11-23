using FluentValidation;
using School_Management_System.Application.DTOs.Courses;

public class UpdateCourseDTOValidator : AbstractValidator<UpdateCourseDTO>
{
    public UpdateCourseDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        
    }
}
