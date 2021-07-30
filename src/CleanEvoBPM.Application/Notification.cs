using MediatR;

namespace CleanEvoBPM.Application
{
    public class Notification<T> : INotification where T : class
    {
        public T Event { get; }
        public Notification(T Event)
        {
            this.Event = Event;
        }
    }
}
