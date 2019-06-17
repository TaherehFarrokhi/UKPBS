using System;

namespace UKPBS.Services.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}