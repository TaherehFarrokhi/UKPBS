using System.Collections.Generic;

namespace UKPBS.Domain.Entities.Events
{
    public class EventCommittee
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<EventInquiry> Inquiries { get; set; }
    }
}