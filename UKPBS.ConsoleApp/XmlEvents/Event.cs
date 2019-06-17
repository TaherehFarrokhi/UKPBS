using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.XmlEvents
{
    [XmlRoot(ElementName = "Event")]
    public class Event
    {
        [XmlElement(ElementName = "StartDate")]
        public string StartDate { get; set; }
        [XmlElement(ElementName = "EndDate")]
        public string EndDate { get; set; }
        [XmlElement(ElementName = "StartTime")]
        public string StartTime { get; set; }
        [XmlElement(ElementName = "EndTime")]
        public string EndTime { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "SortOrder")]
        public string SortOrder { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "House")]
        public string House { get; set; }
        [XmlElement(ElementName = "Category")]
        public string Category { get; set; }
        [XmlElement(ElementName = "HasSpeakers")]
        public string HasSpeakers { get; set; }
        [XmlElement(ElementName = "Committee")]
        public EventCommittee Committee { get; set; }
        [XmlElement(ElementName = "Tags")]
        public EventTags Tags { get; set; }
        [XmlElement(ElementName = "Members")]
        public EventMembers Members { get; set; }
        [XmlElement(ElementName = "Metadata")]
        public EventMetadata Metadata { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }
}