using GogoKit.Models.Response;

namespace ViagogoEventIntegration.Models
{
    public class EventListing
    {
        public EventListing()
        {
        }
        public int? Id { get; set; }
        public Money EstimatedTotalCharge { get; set; }
        public Money TicketPrice { get; set; }
        public int? NumberOfTickets { get; set; }
        public bool? IsPickupAvailable { get; set; }
        public Seating Seating { get; set; }
        public ListingNote[] ListingNotes { get; set; }
    }
}