

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rental.Application.Customers.Queries.GetAllCustomers;
using Rental.Application.Users.Querries;
using Rental.Domain.Entities.Request;
using Rental.Domain.Entities.Response;
using Rental.Domain.Interfaces;

namespace Rental.Application.Users.Queries
{
    public class GetUserQueriesHandler(ILogger<GetAllCustomersQueryHandler> _logger, IMapper _mapper, IUserRepository _userRepository, IUserContext _userContext, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<GetUserQueries, users_response>
    {
        public  async Task<users_response> Handle(GetUserQueries request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting Users.");
            var authuser = _userContext.GetCurrentUser();
            System.Net.IPAddress remoteIpAddress = _httpContextAccessor.HttpContext!.Connection.RemoteIpAddress!.MapToIPv4();
            var header = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var credentialsvalue = header.ToString().Substring("Bearer ".Length).Trim();
            var user_request = _mapper.Map<users_get_request>(request);
            var result = await _userRepository.GetUser(user_request, authuser!.Name, credentialsvalue);
            return result;
        }
    }
}