using AutoMapper;
using Domain.Products;

namespace Application.CQRS.Products.Queries.Retrieve
{
    internal class RetrieveProductQueryResultDtoMapperConfiguration : Profile
    {
        public RetrieveProductQueryResultDtoMapperConfiguration()
        {
            CreateMap<Product, RetrieveProductQueryResultDto>().
                ForMember(dest => dest.Id, cfg => cfg.MapFrom(src => src.Id)).
                ForMember(dest => dest.Name, cfg => cfg.MapFrom(src => src.Name)).
                ForMember(dest => dest.Quantity, cfg => cfg.MapFrom(src => src.Quantity)).
                ForMember(dest => dest.Price, cfg => cfg.MapFrom(src => src.Price));
        }
    }
}
