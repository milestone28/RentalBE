

using AutoMapper;
using Rental.Application.Users.Commands.RegisterUser;
using Rental.Domain.Entities;

namespace Rental.Application.Users.DTOs
{
    public class UserProfile : Profile
    {
       public UserProfile()
        {
            CreateMap<RegisterUserCommand, User>()
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.phone_number))
                .ForMember(d => d.address, opt => opt.MapFrom(s => new Address
                {
                    city = s.city,
                    postal_code = s.postal_code,
                    street = s.street
                }));
        }
    }
}
