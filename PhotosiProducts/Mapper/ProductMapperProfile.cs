using AutoMapper;
using PhotosiProducts.Dto;
using PhotosiProducts.Model;

namespace PhotosiProducts.Mapper;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ReverseMap();
    }
}