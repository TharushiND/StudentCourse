using FluentValidation;
using StudentCourse.DTOs;

namespace StudentCourse.Validators
{
    public class AssignMarksValidator
        : AbstractValidator<AssignMarksDto>
    {
        public AssignMarksValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0);

            RuleFor(x => x.CourseId)
                .GreaterThan(0);

            RuleFor(x => x.Marks)
                .InclusiveBetween(0, 100)
                .WithMessage(
                    "Marks must be between 0 and 100");
        }
    }
}