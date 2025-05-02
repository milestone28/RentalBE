
using FluentValidation;

namespace Rental.Application.Users.Commands.ChangePasswordUser
{
    public class ChangePasswordUserValidators : AbstractValidator<ChangePasswordUserCommand>
    {
        public ChangePasswordUserValidators()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("Old password is required.");
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("New password is required.")
                .MinimumLength(6)
                .WithMessage("New password must be at least 6 characters long.");
            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .WithMessage("Confirm new password is required.")
                .Equal(x => x.NewPassword)
                .WithMessage("New password and confirm new password do not match.");
        }
    }
}
