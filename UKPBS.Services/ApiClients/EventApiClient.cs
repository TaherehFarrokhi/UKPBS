using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using UKPBS.Domain.Entities.Events;
using UKPBS.Services.Exceptions;
using UKPBS.Services.Requests;

namespace UKPBS.Services.ApiClients
{
    public class EventApiClient : ApiClient, IEventApiClient
    {
        public EventApiClient(IRestClient client) : base(client)
        {
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(EventRequest eventRequest)
        {
            if (eventRequest == null) throw new ArgumentNullException(nameof(eventRequest));
            try
            {
                var restRequest = GetEventsRestRequest(eventRequest);
                var events = await ExecuteAsync<List<Event>>(restRequest);
                return events;
            }
            catch (Exception e)
            {
                throw new ExternalServiceException($"Error in retrieving list of events from Event API", e);
            }
        }

        public async Task<Event> GetEventAsync(int id, DateTime startDate)
        {
            try
            {
                var restRequest = GetEventRestRequest(id, startDate);
                var events = await ExecuteAsync<List<Event>>(restRequest);
                return events?.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new ExternalServiceException($"Error in retrieving event {id} from Event API", e);
            }
        }

        private static RestRequest GetEventsRestRequest(EventRequest request)
        {
            var eventRequest = new RestRequest
            {
                Resource = "calendar/events/list.json"
            };
            eventRequest.AddQueryParameter("startDate", request.StartDate.ToString("O"));
            eventRequest.AddQueryParameter("endDate", request.EndDate.ToString("O"));
            eventRequest.AddQueryParameter("house", request.House);
            eventRequest.AddQueryParameter("eventTypeId", request.EventTypeId.ToString());
            return eventRequest;
        }

        private static RestRequest GetEventRestRequest(int eventId, DateTime startDate)
        {
            var request = new RestRequest
            {
                Resource = "calendar/events/list.json"
            };
            request.AddQueryParameter("startDate", startDate.ToString("O"));
            request.AddQueryParameter("eventId", eventId.ToString());
            return request;
        }
    }
}