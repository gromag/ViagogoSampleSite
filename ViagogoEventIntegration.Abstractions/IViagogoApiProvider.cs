using GogoKit;
using GogoKit.Models.Response;

namespace ViagogoEventIntegration.Abstractions
{
    public interface IViagogoApiProvider
    {
        IViagogoClient GetViagogoApiClient();
        OAuth2Token GetToken();
    }
}
