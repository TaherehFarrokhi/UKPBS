using System;
using System.Xml.Serialization;

namespace UKPBS.ConsoleApp.ParliamentMembers
{
    [XmlRoot(ElementName = "Member")]
    public class Member
    {
        [XmlElement(ElementName = "DisplayAs")]
        public string DisplayAs { get; set; }
        [XmlElement(ElementName = "ListAs")]
        public string ListAs { get; set; }
        [XmlElement(ElementName = "FullTitle")]
        public string FullTitle { get; set; }
        [XmlElement(ElementName = "LayingMinisterName")]
        public string LayingMinisterName { get; set; }
        [XmlElement(ElementName = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [XmlElement(ElementName = "DateOfDeath")]
        public DateTime? DateOfDeath { get; set; }
        [XmlElement(ElementName = "Gender")]
        public string Gender { get; set; }
        [XmlElement(ElementName = "Party")]
        public Party Party { get; set; }
        [XmlElement(ElementName = "House")]
        public string House { get; set; }
        [XmlElement(ElementName = "MemberFrom")]
        public string MemberFrom { get; set; }
        [XmlElement(ElementName = "HouseStartDate")]
        public DateTime HouseStartDate { get; set; }
        [XmlElement(ElementName = "HouseEndDate")]
        public DateTime? HouseEndDate { get; set; }
        [XmlElement(ElementName = "CurrentStatus")]
        public CurrentStatus CurrentStatus { get; set; }
        [XmlAttribute(AttributeName = "Member_Id")]
        public string MemberId { get; set; }
        [XmlAttribute(AttributeName = "Dods_Id")]
        public string DodsId { get; set; }
        [XmlAttribute(AttributeName = "Pims_Id")]
        public string PimsId { get; set; }
        [XmlAttribute(AttributeName = "Clerks_Id")]
        public string ClerksId { get; set; }
    }
}