using System.Diagnostics.CodeAnalysis;

namespace PhotosiProducts.Utility;

[ExcludeFromCodeCoverage]
public static class MapperExtension
{
    public static TDestination MapTo<TDestination>(this object source) where TDestination : class
    {
        var mapper = AutoMapper.GetMapper();
        return mapper.Map<TDestination>(source);
    }
}