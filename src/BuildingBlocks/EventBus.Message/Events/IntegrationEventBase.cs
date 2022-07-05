namespace EventBus.Message.Events;

public class IntegrationEventBase
{
	public Guid Id { get; private set; }
	public DateTime CreationDate { get; private set; }

	public IntegrationEventBase()
	{
		Id = Guid.NewGuid();
		CreationDate = DateTime.UtcNow;
	}

	public IntegrationEventBase(Guid id, DateTime creationDate)
	{
		Id = id;
		CreationDate = creationDate;
	}
}