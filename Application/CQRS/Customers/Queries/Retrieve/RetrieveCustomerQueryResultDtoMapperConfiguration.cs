using AutoMapper;
using Domain.Customers;

namespace Application.CQRS.Customers.Queries.Retrieve
{
    internal class RetrieveCustomerQueryResultDtoMapperConfiguration : Profile
    {
        public RetrieveCustomerQueryResultDtoMapperConfiguration()
        {
            CreateMap<Customer, RetrieveCustomerQueryResultDto>().
                ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id)).
                ForMember(dest => dest.FullName, cfg => cfg.MapFrom(src => src.FullName)).
                ForMember(dest => dest.EMail, cfg => cfg.MapFrom(src => src.EMail)).
                ForMember(dest => dest.Cell, cfg => cfg.MapFrom(src => src.Cell));
        }
    }
}
