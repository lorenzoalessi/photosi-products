using System.Diagnostics.CodeAnalysis;

namespace PhotosiProducts.Exceptions;

[ExcludeFromCodeCoverage]
public class CategoryException : Exception
{
    public CategoryException()
    {
    }

    public CategoryException(string message) : base(message)
    {
    }

    public CategoryException(Exception exception)
    {
    }
}