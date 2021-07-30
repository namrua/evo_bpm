using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Domain
{
    public interface IEntity
    {
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
    }
}
