

using MediatR;

namespace Rental.Application.Users.Commands.ChangePasswordUser
{
    public class ChangePasswordUserCommand : IRequest
    {
        public string OldPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        public string ConfirmNewPassword { get; set; } = default!;
    }
}
