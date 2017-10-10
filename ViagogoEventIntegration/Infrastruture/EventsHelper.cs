using System.Collections.Generic;
using System.Linq;
using GogoKit.Models.Response;
using ViagogoEventIntegration.Models;

namespace ViagogoEventIntegration.Infrastruture
{
    public static class EventsHelper
    {

        /// <summary>
        /// Calculate whether an even has the lowest price for every given 
        /// country and populates the number of events within a single country.
        /// Time complexity O(N)
        /// </summary>
        /// <param name="eventInfos"></param>
        public static void AugmentEventInfos(ICollection<EventInfo> eventInfos)
        {
            var pricesByCountry = new Dictionary<string, decimal>();
            var numberOfEventsByCountry = new Dictionary<string, int>();

            foreach (var info in eventInfos)
            {
                var countryCode = info?.Venue?.Country.Code ?? "N/A";
                var ticketPrice = info?.MinTicketPrice?.Amount ?? 0;


                if (!pricesByCountry.ContainsKey(countryCode))
                {
                    pricesByCountry.Add(countryCode, ticketPrice);
                    numberOfEventsByCountry.Add(countryCode, 1);

                    continue;
                }

                numberOfEventsByCountry[countryCode] += 1;

                pricesByCountry[countryCode] = GetLowerPrice(pricesByCountry[countryCode], ticketPrice);

            }

            foreach (var info in eventInfos)
            {
                var countryCode = info?.Venue?.Country?.Code ?? "N/A";
                var ticketPrice = info?.MinTicketPrice?.Amount ?? 0;

                info.IsCountryMinPrice = (ticketPrice == pricesByCountry[countryCode]);
                info.NumberOfEventsInSameCountry = numberOfEventsByCountry[countryCode];
            }
        }
        private static decimal GetLowerPrice(decimal a, decimal b)
        {
            return a < b ? a : b;
        }
    }
}