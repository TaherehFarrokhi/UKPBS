using System.Collections.Generic;
using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.XmlEvents
{
    [XmlRoot(ElementName = "ArrayOfEvent")]
    public class EventList
    {
        [XmlElement(ElementName = "Event")]
        public List<Event> Events { get; set; }
    }
}