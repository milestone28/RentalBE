

using AutoMapper;
using Rental.Application.Users.Commands.RegisterUser;
using Rental.Domain.Entities;

namespace Rental.Application.Users.DTOs
{
    public class UserProfile : Profile
    {
        UserProfile(IUserContext _userContext)
        {
            //var authuser = _userContext.GetCurrentUser();

            CreateMap<RegisterUserCommand, User>()
                .ForMember(d => d.first_name, opt => opt.MapFrom(s => s.first_name))
                .ForMember(d => d.last_name, opt => opt.MapFrom(s => s.last_name))
                .ForMember(d => d.profile_image_url, opt => opt.MapFrom(s => s.profile_image_url))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.email))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.email))
                .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(s => s.email.ToUpper()))
                .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(s => s.email.ToUpper()))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.phone_number))
                .ForMember(d => d.is_deleted, opt => opt.MapFrom(s => false))
                .ForMember(d => d.macaddress_lock, opt => opt.MapFrom(s => false))
                .ForMember(d => d.status, opt => opt.MapFrom(s => false))
                .ForMember(d => d.address, opt => opt.MapFrom(s => new Address
                {
                    city = s.city,
                    postal_code = s.postal_code,
                    street = s.street
                }))
                .ForMember(d => d.date_of_birth, opt => opt.MapFrom(s => s.date_of_birth))
                .ForMember(d => d.last_online, opt => opt.MapFrom(s => (DateTime?)null));
                //.ForMember(d => d.base_model, opt => opt.MapFrom(s => new BaseModel
                //{
                //    created_by = authuser!.Name,
                //    created_date = DateTime.UtcNow.ToLocalTime()
                //}))
        }
    }
}
