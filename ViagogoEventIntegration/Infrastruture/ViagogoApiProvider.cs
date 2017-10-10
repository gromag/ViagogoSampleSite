using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GogoKit;
using GogoKit.Models.Response;
using Microsoft.Ajax.Utilities;
using ViagogoEventIntegration.Abstractions;

namespace ViagogoEventIntegration.Infrastruture
{
    public class ViagogoApiProvider : IViagogoApiProvider
    {
        private readonly CredentialProvider _credentialProvider;
        private ViagogoClient _viagogoClient;
        private OAuth2Token _token;

        public ViagogoApiProvider(CredentialProvider credentialProvider, string myApplicationName)
        {
            _credentialProvider = credentialProvider;
            InitAsync(myApplicationName).GetAwaiter().GetResult();
        }

        protected async Task InitAsync(string myApplicationName)
        {
            if (myApplicationName.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Cannot be null or empty", myApplicationName);
            }

            _viagogoClient = new ViagogoClient(
                new ProductHeaderValue(myApplicationName), 
                _credentialProvider.ClientId, 
                _credentialProvider.ClientSecret);

            _token = await _viagogoClient.OAuth2.GetClientAccessTokenAsync(new List<string>());
            await _viagogoClient.TokenStore.SetTokenAsync(_token);

            
        }

        public IViagogoClient GetViagogoApiClient()
        {
            return _viagogoClient;
        }

        public OAuth2Token GetToken()
        {
            return _token;
        }
    }
}