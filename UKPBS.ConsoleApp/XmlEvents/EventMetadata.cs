using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.XmlEvents
{
    [XmlRoot(ElementName = "Metadata")]
    public class EventMetadata
    {
        [XmlElement(ElementName = "Url")]
        public string Url { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
    }
}