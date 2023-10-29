using MediatR;

namespace Application.Domain.Common;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; }
}

public abstract class DomainEvent : INotification
{
    protected DomainEvent()
    {
        DateOccurred = DateTimeOffset.UtcNow;
    }

    public bool IsPublished { get; set; }
    public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
}