using System;
using GogoKit.Models.Response;

namespace ViagogoEventIntegration.Models
{
    public class EventInfo
    {
        public Money MinTicketPrice { get; set; }
        public string Notes { get; set; }
        public EmbeddedVenue Venue { get; set; }
        public bool IsCountryMinPrice
        { get; set; }

        public int NumberOfEventsInSameCountry { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string TicketsLink { get; internal set; }
        public int? Id { get; internal set; }
    }
}