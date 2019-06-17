using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.ParliamentMembers
{
    [XmlRoot(ElementName = "Members")]
    public class Members
    {
        [XmlElement(ElementName = "Member")]
        public Member Member { get; set; }
    }
}
