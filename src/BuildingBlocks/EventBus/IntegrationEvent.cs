using System;

namespace EventBus
{
    public record IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime created)
        {
            Id = id;
            Created = created;
        }

        public Guid Id { get; private init; }
        public DateTime Created { get; private init; }
    }
}