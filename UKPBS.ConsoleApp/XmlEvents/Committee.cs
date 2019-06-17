using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.XmlEvents
{
    [XmlRoot(ElementName = "Committee")]
    public class EventCommittee
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }
}