using FluentValidation;
using StudentCourse.DTOs;

namespace StudentCourse.Validators
{
    public class DeleteMarksValidator
        : AbstractValidator<DeleteMarksDto>
    {
        public DeleteMarksValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0);

            RuleFor(x => x.CourseId)
                .GreaterThan(0);
        }
    }
}