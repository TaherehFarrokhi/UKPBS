using System.Collections.Generic;
using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.XmlEvents
{
    [XmlRoot(ElementName = "Tags")]
    public class EventTags
    {
        [XmlElement(ElementName = "Tag")]
        public List<EventTag> Tags{ get; set; }
    }
}