namespace CA.And.DDD.Template.Domain
{
    public abstract class Entity
    {
        private List<IDomainEvent> _domainEvents;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;
        protected void AddDomainEvent(IDomainEvent domainEvent)
            => (_domainEvents ??= new List<IDomainEvent>()).Add(domainEvent);
        public void ClearDomainEvents() => this._domainEvents.Clear();
    }
}
