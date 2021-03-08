using System.Collections.Generic;

namespace Accounts.Domain.SeedWork
{
    public abstract class Entity
    {
        private int _id;
        private List<IDomainEvent> _domainEvents;

        public virtual int Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (_domainEvents is null)
            {
                _domainEvents = new List<IDomainEvent>();
            }
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}