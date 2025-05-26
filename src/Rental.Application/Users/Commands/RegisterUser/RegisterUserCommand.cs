

using MediatR;
using Tools.Models.Response;

namespace Rental.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<result_response>
    {
        public string email { get; set; } = default!;
        public string password { get; set; } = default!;
        public string first_name { get; set; } = default!;
        public string last_name { get; set; } = default!;
        public string? profile_image_url { get; set; }
        public string phone_number { get; set; } = default!;
        public string? city { get; set; }
        public string? postal_code { get; set; }
        public string? street { get; set; }
        public DateOnly? date_of_birth { get; set; }
    }
}
