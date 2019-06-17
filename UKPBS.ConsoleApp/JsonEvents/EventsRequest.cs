using System;

namespace UKPBS.ConsoleApp.JsonEvents
{
    public class EventsRequest
    {
        public string Format { get; set; }
        public string House { get; set; }
        public int EventTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Date { get; set; }
        public int LocationId { get; set; }
        public int MemberId { get; set; }
        public string Tag { get; set; }
        public int CommitteeId { get; set; }
        public int InquiryId { get; set; }
        public int CategoryId { get; set; }
        public int EventId { get; set; }
        public string CategoryCode { get; set; }
    }
}