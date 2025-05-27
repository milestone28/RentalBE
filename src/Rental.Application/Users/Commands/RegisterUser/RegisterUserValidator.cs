using FluentValidation;
using System.Text.RegularExpressions;

namespace Rental.Application.Users.Commands.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator() 
        {

            RuleFor(x => x.first_name)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.last_name)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(100).WithMessage("Username must not exceed 100 characters.");
            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
                .Must(ContainDigit)
                .WithMessage("Password must contain at least one digit.")
                .Must(ContainSpecialCharacter)
                .WithMessage("Password must contain at least one special character (e.g., !@#$%^&*).");
            RuleFor(x => x.phone_number)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^(?:\+63|0)[\s\-]?[2-9]\d{1,2}[\s\-]?\d{3,4}[\s\-]?\d{4}$").WithMessage("Invalid contact number format. ex. +639123456789");
            RuleFor(x => x.city)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");
            RuleFor(x => x.street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(100).WithMessage("Street must not exceed 100 characters.");
            RuleFor(x => x.postal_code)
                .NotEmpty().WithMessage("Postal code is required.")
                .Matches(@"^\d{4}$").WithMessage("Invalid postal code format (****) should be four digit.");
        }

        private bool ContainDigit(string password)
        {
            return Regex.IsMatch(password, @"[0-9]");
        }

        private bool ContainSpecialCharacter(string password)
        {
            return Regex.IsMatch(password, @"[!@#$%^&*()_+{}\[\]:;<>,.?/~`\-]");
        }
    }
}
