using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Exceptions;

namespace Order.Application.Behaviours;

public sealed class UnhandledBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            var requestName = nameof(TRequest);
            _logger.LogError(e, "Unhandled Exception for Request {Name} {@Request}", requestName, request);
            throw new ServerException();
        }
    }
}