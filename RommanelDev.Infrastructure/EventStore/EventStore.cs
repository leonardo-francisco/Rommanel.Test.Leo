using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Infrastructure.EventStore
{
    public class EventStore : IEventStore
    {
        private readonly List<INotification> _eventStore = new();

        public async Task SaveAsync(INotification @event)
        {
            _eventStore.Add(@event);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<INotification>> GetEventsAsync(string aggregateId)
        {
            return await Task.FromResult(_eventStore.Where(e =>
                (e as dynamic).ClienteId == aggregateId));
        }
    }
}
