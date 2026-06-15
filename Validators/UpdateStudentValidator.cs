using FluentValidation;
using StudentCourse.DTOs;

namespace StudentCourse.Validators
{
    public class UpdateStudentValidator
        : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Age)
                .InclusiveBetween(18, 100);
        }
    }
}