using AutoMapper;
using Domain.Products;

namespace Application.CQRS.Products.Queries.ListAll
{
    internal class ListAllProductQueryResultDtoMapperConfiguration : Profile
    {
        public ListAllProductQueryResultDtoMapperConfiguration()
        {
            CreateMap<Product, ListAllProductQueryResultDto>().
                ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id)).
                ForMember(dest => dest.Name, cfg => cfg.MapFrom(src => src.Name)).
                ForMember(dest => dest.Quantity, cfg => cfg.MapFrom(src => src.Quantity)).
                ForMember(dest => dest.Price, cfg => cfg.MapFrom(src => src.Price));
        }
    }
}
