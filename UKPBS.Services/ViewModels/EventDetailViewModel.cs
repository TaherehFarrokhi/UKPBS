using System.Collections.Generic;

namespace UKPBS.Services.ViewModels
{
    public class EventDetailViewModel: EventViewModel
    {
        public string Category { get; set; }
        public IList<MemberViewModel> Members { get; set; }
    }
}