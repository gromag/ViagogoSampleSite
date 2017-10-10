using System.Configuration;
using ViagogoEventIntegration.Abstractions;

namespace ViagogoEventIntegration.Infrastruture
{
    public class CredentialProvider : ICredentialProvider
    {

        public CredentialProvider()
        {
            this.ClientId = ConfigurationManager.AppSettings["Viagogo.OAuth2.ClientId"];
            this.ClientSecret = ConfigurationManager.AppSettings["Viagogo.OAuth2.ClientSecret"];
        }

        public string ClientId { get; }
        public string ClientSecret { get; }
    }
}