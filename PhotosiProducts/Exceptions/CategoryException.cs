namespace PhotosiProducts.Exceptions;

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