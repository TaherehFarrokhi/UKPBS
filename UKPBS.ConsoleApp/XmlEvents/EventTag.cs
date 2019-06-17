using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.XmlEvents
{
    [XmlRoot(ElementName = "Tag")]
    public class EventTag
    {
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
    }
}