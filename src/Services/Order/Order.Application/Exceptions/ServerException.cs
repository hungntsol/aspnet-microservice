namespace Order.Application.Exceptions;

public class ServerException : Exception
{
    public ServerException(string message) : base(message)
    {
        
    }

    public ServerException()
    {
        
    }
}