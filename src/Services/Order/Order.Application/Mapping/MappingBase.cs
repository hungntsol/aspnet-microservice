using Mapster;

namespace Order.Application.Mapping;

public abstract class MappingBase<TDto, TEntity> : IRegister where TDto : class where TEntity : class
{
    public static TEntity ToEntity(TDto dto)
    {
        return dto.Adapt<TEntity>();
    }

    public static TDto FromEntity(TEntity entity)
    {
        return entity.Adapt<TDto>();
    }
    
    private TypeAdapterConfig _config { get; set; }

    protected TypeAdapterSetter<TDto, TEntity> SetMapping() => _config.ForType<TDto, TEntity>();
    protected TypeAdapterSetter<TEntity, TDto> SetMappingInverse() => _config.ForType<TEntity, TDto>();

    public virtual void AddMapping()
    {
        SetMapping();
        SetMappingInverse();
    }
    
    public void Register(TypeAdapterConfig config)
    {
        _config = config;
        AddMapping();
    }
}