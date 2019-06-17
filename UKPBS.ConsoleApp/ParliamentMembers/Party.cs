using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.ParliamentMembers
{
    [XmlRoot(ElementName = "Party")]
    public class Party
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Content = true)]
        public string Text { get; set; }
    }
}