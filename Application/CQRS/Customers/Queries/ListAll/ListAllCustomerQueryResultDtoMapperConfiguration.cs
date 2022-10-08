using AutoMapper;
using Domain.Customers;

namespace Application.CQRS.Customers.Queries.ListAll
{
    internal class ListAllCustomerQueryResultDtoMapperConfiguration : Profile
    {
        public ListAllCustomerQueryResultDtoMapperConfiguration()
        {
            CreateMap<Customer, ListAllCustomerQueryResultDto>().
                ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id)).
                ForMember(dest => dest.FullName, cfg => cfg.MapFrom(src => src.FullName)).
                ForMember(dest => dest.EMail, cfg => cfg.MapFrom(src => src.EMail)).
                ForMember(dest => dest.Cell, cfg => cfg.MapFrom(src => src.Cell));
        }
    }
}
