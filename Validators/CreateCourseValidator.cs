using FluentValidation;
using StudentCourse.DTOs;

namespace StudentCourse.Validators
{
    public class CreateCourseValidator
        : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseValidator()
        {
            RuleFor(x => x.CourseName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Duration)
                .NotEmpty();
        }
    }
}