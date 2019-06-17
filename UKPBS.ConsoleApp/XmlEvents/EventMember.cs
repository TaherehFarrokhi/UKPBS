using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.XmlEvents
{
    [XmlRoot(ElementName = "Member")]
    public class EventMember
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "SortOrder")]
        public string SortOrder { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }
}