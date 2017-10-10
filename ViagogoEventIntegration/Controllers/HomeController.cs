using Microsoft.Ajax.Utilities;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using ViagogoEventIntegration.Abstractions;
using ViagogoEventIntegration.Repositories;


namespace ViagogoEventIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventRepo _eventRepo;

        public HomeController(IEventRepo eventRepo)
        {
            _eventRepo = eventRepo;
        }

        public async Task<ActionResult> Index()
        {
            await Task.CompletedTask;
            return View();
        }

        public async Task<ActionResult> Search(string searchTerms)
        {
            if (searchTerms.IsNullOrWhiteSpace())
            {
                RedirectToAction("Index");
            }
            
            var results = await _eventRepo.GetEventDetails(searchTerms);

            if (results == null)
            {
                RedirectToAction("Index", new RouteValueDictionary() {{"outcome", "not-event-found"}});
            }

            return View(results);

        }
        [Route("Home/GetListings/{id}")]
        public async Task<ActionResult> GetListings(int id)
        {
            var listings = await _eventRepo.GetEventListings(id);

            return PartialView(listings);
        }
    }
}
