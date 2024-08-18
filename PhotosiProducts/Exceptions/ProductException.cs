using System.Diagnostics.CodeAnalysis;

namespace PhotosiProducts.Exceptions;

[ExcludeFromCodeCoverage]
public class ProductException : Exception
{
    public ProductException()
    {
    }

    public ProductException(string message) : base(message)
    {
    }

    public ProductException(Exception exception)
    {
    }
}