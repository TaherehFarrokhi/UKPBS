using System.Xml.Serialization;

namespace UKPBS.Domain.Entities.ParliamentMembers
{
    [XmlRoot(ElementName = "Party")]
    public class Party
    {
        [XmlAttribute(AttributeName = "Id")]
        public int Id { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Content = true)]
        public string Text { get; set; }
    }
}