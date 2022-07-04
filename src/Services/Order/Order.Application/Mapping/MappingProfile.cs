using Mapster;
using Order.Application.Features.Orders.Commands.Checkout;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using Order.Application.Features.Orders.Queries.Vms;

namespace Order.Application.Mapping;

public sealed class MappingProfile : IRegister
{
    private TypeAdapterConfig _config = null!;

    private TypeAdapterSetter<TDto, TEntity> SetMapping<TDto, TEntity>()
        => _config.ForType<TDto, TEntity>();

    private TypeAdapterSetter<TEntity, TDto> SetMappingIncludeInverse<TDto, TEntity>()
        => _config.ForType<TEntity, TDto>();


    public void Register(TypeAdapterConfig config)
    {
        _config = config;

        // entity to view model
        SetMapping<Domain.Entities.Order, OrderViewModel>();
        
        // command to model
        SetMapping<CheckoutOrderCommand, Domain.Entities.Order>();
        SetMapping<UpdateOrderCommand, Domain.Entities.Order>();
    }
}