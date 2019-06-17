using System;
using System.Xml.Serialization;

namespace UKPBS.Domain.Entities.ParliamentMembers
{
    [XmlRoot(ElementName = "CurrentStatus")]
    public class CurrentStatus
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Reason")]
        public string Reason { get; set; }
        [XmlElement(ElementName = "StartDate")]
        public DateTime? StartDate { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "IsActive")]
        public bool IsActive { get; set; }
    }
}