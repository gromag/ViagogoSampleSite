using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GogoKit;
using GogoKit.Services;
using ViagogoEventIntegration.Infrastruture;

namespace ViagogoEventIntegration.Tests
{
    [TestFixture]
    public class ViagogoClientIntegrationTests
    {
        private const string AppName = "ViagogoExploratoryTest";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldReceiveResultsFromViagogoApi()
        {
            var credentialProvider = new CredentialProvider(); ;

            //Given
            var sut = new ViagogoClient(new ProductHeaderValue(AppName), credentialProvider.ClientId, credentialProvider.ClientSecret);

            //When
            var root = await sut.Hypermedia.GetRootAsync();

            //Then
            root.Should().NotBe(null);
        }

        [Test]
        public async Task ShouldBeAbleToSetAToken()
        {
            var credentialProvider = new CredentialProvider(); ;

            //Given
            var sut = new ViagogoClient(new ProductHeaderValue(AppName), credentialProvider.ClientId, credentialProvider.ClientSecret);

            //When
            Func<Task> asyncFunc = async () =>
            {
                var token = await sut.OAuth2.GetClientAccessTokenAsync(new List<string>());
                await sut.TokenStore.SetTokenAsync(token);
            };

            //Then
            asyncFunc.ShouldNotThrow();
        }

    }
}
