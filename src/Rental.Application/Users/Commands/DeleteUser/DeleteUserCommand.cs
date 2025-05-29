

using MediatR;
using Tools.Models.Response;

namespace Rental.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<result_response>
    {
        public string user_id { get; set; } = default!;
    }
}
