using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKPBS.Domain.Entities.Events;
using UKPBS.Services.Requests;

namespace UKPBS.Services.ApiClients
{
    public interface IEventApiClient
    {
        Task<IEnumerable<Event>> GetEventsAsync(EventRequest eventRequest);
        Task<Event> GetEventAsync(int id, DateTime startDate);
    }
}