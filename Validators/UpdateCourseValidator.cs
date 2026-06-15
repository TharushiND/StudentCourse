using FluentValidation;
using StudentCourse.DTOs;

namespace StudentCourse.Validators
{
    public class UpdateCourseValidator
        : AbstractValidator<UpdateCourseDto>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.CourseName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Duration)
                .NotEmpty();
        }
    }
}