using System.Reflection;
using FluentValidation;
using MediatR;
using Order.Application.Behaviours;

namespace Order.API.ExtensionMethods;

public static class AddPipelineExtension
{
    public static IServiceCollection AddPipeline(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return services;
    }
}