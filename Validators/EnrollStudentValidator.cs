using FluentValidation;
using StudentCourse.DTOs;

namespace StudentCourse.Validators
{
    public class EnrollStudentValidator
        : AbstractValidator<EnrollStudentDto>
    {
        public EnrollStudentValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.CourseId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}