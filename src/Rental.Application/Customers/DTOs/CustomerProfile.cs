
using AutoMapper;
using Rental.Application.Customers.Command.CreateCustomer;
using Rental.Application.Customers.Command.TempDeleteCustomer;
using Rental.Application.Customers.Command.UpdateCustomer;
using Rental.Application.Customers.Command.UploadCustomerImage;
using Rental.Domain.Entities;

namespace Rental.Application.Customers.DTOs
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<Customer, CustomersDtos>()
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.City))
                .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.PostalCode))
                .ForMember(d => d.Street, opt => opt.MapFrom(s => s.Address == null ? null : s.Address.Street))
                .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => s.DateCreated.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(d => d.DateUpdated, opt => opt.MapFrom(s => s.DateUpdated.HasValue ? s.DateUpdated.Value.ToString("yyyy-MM-dd HH:mm:ss") : null))
                .ForMember(d => d.DateDeleted, opt => opt.MapFrom(s => s.DateDeleted.HasValue ? s.DateDeleted.Value.ToString("yyyy-MM-dd HH:mm:ss") : null));

            CreateMap<CreateCustomerCommand, Customer>()
                .ForMember(d => d.DateCreated, opt => opt.MapFrom(s => DateTime.UtcNow.ToLocalTime()))
                .ForMember(d => d.DateUpdated, opt => opt.MapFrom(s => (DateTime?)null))
                .ForMember(d => d.DateDeleted, opt => opt.MapFrom(s => (DateTime?)null))
                .ForMember(d => d.IsDeleted, opt => opt.MapFrom(s => false))
                .ForMember(d => d.Address, opt => opt.MapFrom(s => new Address
                {
                    City = s.City,
                    PostalCode = s.PostalCode,
                    Street = s.Street
                }));

            CreateMap<UpdateCustomerCommand, Customer>()
                .ForMember(d => d.DateUpdated, opt => opt.MapFrom(s => DateTime.UtcNow.ToLocalTime()));

            CreateMap<TempDeleteCustomerCommand, Customer>()
                 .ForMember(d => d.DateDeleted, opt => opt.MapFrom(s => DateTime.UtcNow.ToLocalTime()))
                 .ForMember(d => d.IsDeleted, opt => opt.MapFrom(s => true));
        }
    }
}
