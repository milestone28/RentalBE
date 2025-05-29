

using FluentValidation;

namespace Rental.Application.Users.Commands.DeleteUser
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.user_id).NotEmpty().WithMessage("User ID cannot be empty.");
        }
    }
}
