using System.Collections.Generic;

namespace UKPBS.ConsoleApp.JsonEvents
{
    public class NewEventCommittee
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<NewEventInquiry> Inquiries { get; set; }
    }
}