using System;

namespace UKPBS.Services.Requests
{
    public class EventRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string House { get; set; } = "Commons";
        public int EventTypeId { get; set; } = 32;
    }
}
