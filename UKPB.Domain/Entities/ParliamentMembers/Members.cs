using System.Xml.Serialization;

namespace UKPBS.Domain.Entities.ParliamentMembers
{
    [XmlRoot(ElementName = "Members")]
    public class Members
    {
        [XmlElement(ElementName = "Member")]
        public Member Member { get; set; }
    }
}
