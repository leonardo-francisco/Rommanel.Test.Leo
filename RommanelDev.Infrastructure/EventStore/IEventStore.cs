using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Infrastructure.EventStore
{
    public interface IEventStore
    {
        Task SaveAsync(INotification @event);
        Task<IEnumerable<INotification>> GetEventsAsync(string aggregateId);
    }
}
