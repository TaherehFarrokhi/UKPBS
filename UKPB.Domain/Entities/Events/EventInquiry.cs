namespace UKPBS.Domain.Entities.Events
{
    public class EventInquiry
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}