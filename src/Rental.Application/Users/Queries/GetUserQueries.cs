

using MediatR;
using Rental.Domain.Entities.Response;
using static Tools.Models.sort_direction;

namespace Rental.Application.Users.Querries
{
    public class GetUserQueries : IRequest<users_response>
    {
        public string? search_phrase { get; set; } 
        public string? sort_by { get; set; }
        public SortDirection sort_direction { get; set; }
        public int page_number { get; set; }
        public int page_size { get; set; }
    }
}
