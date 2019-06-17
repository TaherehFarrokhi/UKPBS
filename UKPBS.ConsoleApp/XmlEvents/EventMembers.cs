using System.Collections.Generic;
using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.XmlEvents
{
    [XmlRoot(ElementName = "Members")]
    public class EventMembers
    {
        [XmlElement(ElementName = "Member")]
        public List<EventMember> Members { get; set; }
    }
}