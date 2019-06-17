using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UKPBS.Services.Requests;
using UKPBS.Services.Responses;
using UKPBS.Services.ViewModels;

namespace UKPBS.Services
{
    public interface IEventService
    {
        Task<ClientResponse<IEnumerable<EventViewModel>>> GetEventsAsync(EventRequest eventRequest);
        Task<ClientResponse<EventDetailViewModel>> GetEventDetailAsync(int id, DateTime startDate);
    }
}