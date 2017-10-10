using System;
using System.Collections.Generic;
using GogoKit.Models.Response;

namespace ViagogoEventIntegration.Models
{
    public class CategoryInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Image MainImage { get; set; }

        public DateTimeOffset? MaxEventDate { get; set; }

        public Money MinTicketPrice { get; set; }
        public List<EventInfo> EventDetails { get; set; }
    }
}