﻿using System;
using System.Collections.Generic;

namespace UKPBS.Domain.Entities.Events
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int SortOrder { get; set; }
        public string Type { get; set; }
        public string House { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string LocationMetadata { get; set; }
        public bool HasSpeakers { get; set; }
        public EventCommittee Committee { get; set; }
        public List<EventTag> Tags { get; set; }
        public List<EventMember> Members { get; set; }
        public List<EventMetadata> Metadata { get; set; }
        public string SummarisedDetails { get; set; }
    }
}