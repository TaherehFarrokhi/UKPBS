using System;
using System.Collections.Generic;
using RestSharp;
using UKPBS.Domain.Entities.Events;
using UKPBS.Domain.Entities.ParliamentMembers;

namespace UKPBS.ConsoleApp
{
    class Program
    {
        const string MemberBaseUrl = "http://data.parliament.uk";
        const string EventsUrl = "http://service.calendar.parliament.uk";

        static void Main(string[] args)
        {
//            var xmlEvents = GetXMLEvents();
            var jsonEvents = GetJsonEvents();
            var memberId = 579;
            var member = GetMember(memberId);
        }

        private static Member GetMember(int id)
        {
            var request = new RestRequest
            {
                Resource = $"membersdataplatform/services/mnis/members/query/id={id}"
            };
            request.AddHeader("Accept", "application/xml");

            return new ApiClient(MemberBaseUrl).Execute<Members>(request).Member;
        }

//        private static EventList GetXMLEvents()
//        {
//            var eventRequest = BuildEventRequest("calendar/events/list.xml");
//            return new ApiClient(EventsUrl).Execute<EventList>(eventRequest);
//        }

        private static List<Event> GetJsonEvents()
        {
            var eventRequest = BuildEventRequest("calendar/events/list.json");
            return new ApiClient(EventsUrl).Execute<List<Event>>(eventRequest);
        }

        private static RestRequest BuildEventRequest(string resource)
        {
            var eventRequest = new RestRequest
            {
                Resource = resource
            };
            var startDate = new DateTime(2015, 11, 16);
            var endDate = new DateTime(2015, 11, 26);
            eventRequest.AddQueryParameter("startdate", startDate.ToString("O"));
            eventRequest.AddQueryParameter("enddate", endDate.ToString("O"));
            eventRequest.AddQueryParameter("house", "Commons");
            eventRequest.AddQueryParameter("eventTypeId", "32");
            return eventRequest;
        }
    }
}
