

using AutoMapper;
using Rental.Application.Users.Commands.DeleteUser;
using Rental.Application.Users.Commands.RegisterUser;
using Rental.Application.Users.Commands.UpdateUser;
using Rental.Application.Users.Querries;
using Rental.Domain.Entities;
using Rental.Domain.Entities.Request;
using Rental.Domain.Entities.Response;

namespace Rental.Application.Users.DTOs
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DeleteUserCommand, user_id_request>()
                .ForMember(d => d.user_id, opt => opt.MapFrom(s => s.user_id));

            CreateMap<RegisterUserCommand, User>()
                .ForMember(d => d.mobile, opt => opt.MapFrom(s => s.mobile))
                .ForMember(d => d.address, opt => opt.MapFrom(s => new Address
                {
                    city = s.city,
                    postal_code = s.postal_code,
                    street = s.street
                }));

            CreateMap<GetUserQueries, users_get_request>()
                .ForMember(d => d.search, opt => opt.MapFrom(s => s.search_phrase))
                .ForMember(d => d.sort_by, opt => opt.MapFrom(s => s.sort_by))
                .ForMember(d => d.sort_direction, opt => opt.MapFrom(s => s.sort_direction))
                .ForMember(d => d.page_no, opt => opt.MapFrom(s => s.page_number))
                .ForMember(d => d.page_size, opt => opt.MapFrom(s => s.page_size));

            CreateMap<User, user_map_response>()
            .ForMember(d => d.city, opt => opt.MapFrom(s => s.address.city))
            .ForMember(d => d.postal_code, opt => opt.MapFrom(s => s.address.postal_code))
            .ForMember(d => d.street, opt => opt.MapFrom(s => s.address.street));

            CreateMap<UpdateUserCommand, user_request>()
             .ForMember(d => d.address, opt => opt.MapFrom(s => new Address
            {
                city = s.city,
                postal_code = s.postal_code,
                street = s.street
            }));
            //.ForMember(d => d.address.city, opt => opt.MapFrom(s => s.city))
            //.ForMember(d => d.address.postal_code, opt => opt.MapFrom(s => s.postal_code))
            //.ForMember(d => d.address.street, opt => opt.MapFrom(s => s.street));
        }
    }
}
