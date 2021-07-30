using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Domain
{
    public interface INotificationDispatcher
    {
        Task Push(IDomainEvent devent);
    }
}
