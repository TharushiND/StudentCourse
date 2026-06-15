using FluentValidation;
using StudentCourse.DTOs;

namespace StudentCourse.Validators
{
    public class CreateStudentValidator
        : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email format");

            RuleFor(x => x.Age)
                .InclusiveBetween(18, 100)
                .WithMessage("Age must be between 18 and 100");
        }
    }
}