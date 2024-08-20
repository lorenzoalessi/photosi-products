using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using PhotosiProducts.Dto;
using PhotosiProducts.Model;

namespace PhotosiProducts.Mapper;

[ExcludeFromCodeCoverage]
public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ReverseMap();
    }
}