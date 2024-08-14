namespace PhotosiProducts.Exceptions;

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