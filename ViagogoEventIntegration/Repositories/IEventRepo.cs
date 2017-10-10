using System.Collections.Generic;
using System.Threading.Tasks;
using ViagogoEventIntegration.Models;

namespace ViagogoEventIntegration.Repositories
{
    public interface IEventRepo
    {
        Task<CategoryInfo> GetEventDetails(string searchTerms);
        Task<List<EventListing>> GetEventListings(int id);
    }
}