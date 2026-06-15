using FluentValidation;
using StudentCourse.DTOs;

namespace StudentCourse.Validators
{
    public class UpdateMarksValidator
        : AbstractValidator<UpdateMarksDto>
    {
        public UpdateMarksValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0);

            RuleFor(x => x.CourseId)
                .GreaterThan(0);

            RuleFor(x => x.Marks)
                .InclusiveBetween(0, 100);
        }
    }
}