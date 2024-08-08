using System.Diagnostics.CodeAnalysis;
using PhotosiProducts.Mapper;
using AutoMapper;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PhotosiProducts.Utility;

[ExcludeFromCodeCoverage]
public static class AutoMapper
{
    private static readonly IConfigurationProvider Config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(typeof(ProductMapperProfile));
    });

    public static IMapper GetMapper()
    {
        var mapper = Config.CreateMapper();
        return mapper;
    }
}