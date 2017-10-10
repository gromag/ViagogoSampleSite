using GogoKit;
using GogoKit.Models.Response;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViagogoEventIntegration.Abstractions;
using ViagogoEventIntegration.Infrastruture;
using ViagogoEventIntegration.Models;

namespace ViagogoEventIntegration.Repositories
{
    public class EventRepo : IEventRepo
    {
        private const string Category = "Category";
        private const string Artist = "Artist";
        private readonly IViagogoApiProvider _viagogoApiProvider;

        public EventRepo(IViagogoApiProvider viagogoApiProvider)
        {
            _viagogoApiProvider = viagogoApiProvider;
        }

        public async Task<CategoryInfo> GetEventDetails(string searchTerms)
        {
            //Validating input
            if (searchTerms.IsNullOrWhiteSpace())
            {
                return null; ;
            }

            var api = _viagogoApiProvider.GetViagogoApiClient();

            //Searching on search terms
            var searchResult = await api.Search.GetAllAsync(searchTerms);

            if (searchResult.Count == 0)
            {
                return null;
            }

            //Extracting artist node from search terms
            var artistSearchResult = searchResult
                .FirstOrDefault(sr => sr.Type == Category && sr.TypeDescription == Artist);

            if (artistSearchResult == null || artistSearchResult.CategoryLink == null)
            {
                return null;
            }

            //Fetching artist details
            var artistDetails = await api.Hypermedia
                .GetAsync<Category>(artistSearchResult.CategoryLink);

            if (artistDetails == null || artistDetails.EventsLink == null)
            {
                return null;
            }

            //Fetching events for that artist
            var events = await api.Hypermedia
                .GetAllPagesAsync<Event>(artistDetails.EventsLink);

            var eventDetails = new CategoryInfo()
            {
                Title = artistDetails.Name,
                Description = artistDetails.Description,
                MainImage = new Image()
                {
                    Href = artistDetails.ImageLink.HRef,
                    Title = artistDetails.ImageLink.Title
                },
                MaxEventDate = artistDetails.MaxEventDate,
                MinTicketPrice = artistDetails.MinTicketPrice,
                EventDetails = events.Select(e => new EventInfo()
                {
                    MinTicketPrice = e.MinTicketPrice,
                    Notes = e.Notes,
                    Venue = e.Venue,
                    IsCountryMinPrice = false,
                    NumberOfEventsInSameCountry = 1,
                    Date = e.StartDate,
                    TicketsLink = e.ListingsLink?.HRef ?? "",
                    Id = e.Id

                }).ToList()
            };

            EventsHelper.AugmentEventInfos(eventDetails.EventDetails);

            return eventDetails;
        }

        public async Task<List<EventListing>> GetEventListings(int id)
        {

            var api = _viagogoApiProvider.GetViagogoApiClient();

            try
            {

                var listings = await api.Listings.GetAllByEventAsync(id);

                if (!listings.Any())
                {
                    return null;
                }

                var eventListings = listings.Select(l => new EventListing()
                {
                    Id = l.Id,
                    EstimatedTotalCharge = l.EstimatedTotalCharge,
                    TicketPrice = l.TicketPrice,
                    NumberOfTickets = l.NumberOfTickets,
                    IsPickupAvailable = l.IsPickupAvailable,
                    Seating = l.Seating,
                    ListingNotes = l.ListingNotes
                }).ToList();

                return eventListings;
            }
            catch (GogoKit.Exceptions.ResourceNotFoundException e)
            {
                return null;
            }
        }

        private bool ValidateUrl(string url)
        {
            //TODO : Proper validation required
            return url.IsNullOrWhiteSpace();
        }
    }
}