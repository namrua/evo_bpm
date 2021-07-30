using CleanEvoBPM.Domain;
using MediatR;
using System;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application
{
    public class NotificationDispatcher : INotificationDispatcher
    {
        private readonly IMediator _mediator;
        public NotificationDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Push(IDomainEvent devent)
        {

            var domainEventNotification = _createDomainEventNotification(devent);
            await _mediator.Publish(domainEventNotification);
        }

        private INotification _createDomainEventNotification(IDomainEvent domainEvent)
        {
            var genericDispatcherType = typeof(Notification<>).MakeGenericType(domainEvent.GetType());
            return (INotification)Activator.CreateInstance(genericDispatcherType, domainEvent);
        }
    }
}
