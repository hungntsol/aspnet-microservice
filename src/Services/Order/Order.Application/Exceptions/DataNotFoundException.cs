namespace Order.Application.Exceptions;

public class DataNotFoundException : Exception
{
    public DataNotFoundException(string message) : base(message)
    {
    }

    public DataNotFoundException() : base("Opps, data is not found")
    {
        
    }
}