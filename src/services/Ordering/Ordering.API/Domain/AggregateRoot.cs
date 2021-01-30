﻿using System.Collections.Generic;

namespace TooBigToFailBurgerShop.Ordering.Domain
{
    public abstract class AggregateRoot<TType, TKey> : Entity<TKey>, IAggregateRoot<TKey> where TType : class, IAggregateRoot<TKey>
    {
        public long Version { get; private set; }

        public IReadOnlyCollection<IDomainEvent<TKey>> Events => _events.ToImmutableArray();

        private readonly Queue<IDomainEvent<TKey>> _events = new Queue<IDomainEvent<TKey>>();

        protected AggregateRoot(TKey id) : base(id)
        {

        }

        protected void AddEvent(IDomainEvent<TKey> domainEvent)
        {
            _events.Enqueue(domainEvent);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}