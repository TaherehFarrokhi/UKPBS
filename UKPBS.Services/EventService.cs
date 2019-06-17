using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UKPBS.Domain.Entities.Events;
using UKPBS.Domain.Entities.ParliamentMembers;
using UKPBS.Services.ApiClients;
using UKPBS.Services.Helpers;
using UKPBS.Services.Requests;
using UKPBS.Services.Responses;
using UKPBS.Services.ViewModels;

namespace UKPBS.Services
{
    public class EventService : IEventService
    {
        private readonly IEventApiClient _eventApiClient;
        private readonly ILogger<EventService> _logger;
        private readonly IMemberApiClient _memberApiClient;

        public EventService(IEventApiClient eventApiClient, IMemberApiClient memberApiClient,
            ILogger<EventService> logger)
        {
            _eventApiClient = eventApiClient ?? throw new ArgumentNullException(nameof(eventApiClient));
            _memberApiClient = memberApiClient ?? throw new ArgumentNullException(nameof(memberApiClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ClientResponse<IEnumerable<EventViewModel>>> GetEventsAsync(EventRequest eventRequest)
        {
            try
            {
                var events = await _eventApiClient.GetEventsAsync(eventRequest);
                var payload = MapToEventViewModel(events);

                return ResponseHelper.SuccessFrom(payload);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return ResponseHelper.FailFrom<IEnumerable<EventViewModel>>(e.Message);
            }
        }

        public async Task<ClientResponse<EventDetailViewModel>> GetEventDetailAsync(int id, DateTime startDate)
        {
            try
            {
                var @event = await _eventApiClient.GetEventAsync(id, startDate);
                if (@event == null)
                    return ResponseHelper.FailFrom<EventDetailViewModel>(
                        "Event Not Found.");

                var memberIds = @event.Members?.Select(x => x.Id).ToArray();
                IEnumerable<Member> members = null;
                if (memberIds?.Any() == true)
                {
                     members= _memberApiClient.GetMembersByIds(memberIds);
                }
                var mapToEventViewModel = MapToEventViewModel(@event, members);
                return ResponseHelper.SuccessFrom(mapToEventViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return ResponseHelper.FailFrom<EventDetailViewModel>(e.Message);
            }
        }

        private static EventDetailViewModel MapToEventViewModel(Event @event, IEnumerable<Member> members)
        {
            return new EventDetailViewModel
            {
                Id = @event.Id,
                StartDate = TimeSpan.TryParse(@event.StartTime, out var startTime)
                    ? @event.StartDate + startTime
                    : @event.StartDate,
                EndDate = TimeSpan.TryParse(@event.EndTime, out var endTime) && @event.EndDate.HasValue
                    ? @event.EndDate + endTime
                    : @event.EndDate,
                Description = @event.Description,
                Category = @event.Category,
                Members = members?.Select(m => new MemberViewModel
                {
                    MemberFrom = m.CurrentStatus?.StartDate,
                    FullTitle = m.FullTitle,
                    Party = m.Party?.Text
                }).ToList()
            };
        }

        private static IEnumerable<EventViewModel> MapToEventViewModel(IEnumerable<Event> events)
        {
            return events.Select(e => new EventViewModel
            {
                Id = e.Id,
                StartDate = TimeSpan.TryParse(e.StartTime, out var startTime) ? e.StartDate + startTime : e.StartDate,
                EndDate = TimeSpan.TryParse(e.EndTime, out var endTime) && e.EndDate.HasValue
                    ? e.EndDate + endTime
                    : e.EndDate,
                Description = e.Description
            }).ToList();
        }
    }
}