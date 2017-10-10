using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using GogoKit;
using GogoKit.Models.Response;
using Moq;
using ViagogoEventIntegration.Abstractions;
using ViagogoEventIntegration.Controllers;
using ViagogoEventIntegration.Infrastruture;
using ViagogoEventIntegration.Repositories;

namespace ViagogoEventIntegration.Tests
{
    [TestFixture]
    public class AnEventRepo
    {
        private ViagogoClient _viagogoClient;
        private Mock<IReadOnlyList<SearchResult>> _mockResults;
        private Mock<IViagogoClient> _mockApi;
        private Mock<IViagogoApiProvider> _mockApiProvider;

        [SetUp]
        public void Setup()
        {
            var credentialProvider = new CredentialProvider(); ;
            _viagogoClient = new ViagogoClient(new ProductHeaderValue("MyApplication"), credentialProvider.ClientId, credentialProvider.ClientSecret);

            _mockResults = new Mock<IReadOnlyList<SearchResult>>();

            _mockApi = new Mock<IViagogoClient>();
            _mockApi
                .Setup(m => m.Search.GetAllAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(_mockResults.Object));

            _mockApiProvider = new Mock<IViagogoApiProvider>();
            _mockApiProvider
                .Setup(m => m.GetViagogoApiClient())
                .Returns(_mockApi.Object);
        }

        [Test]
        public async Task ShouldNotCallTheApiIfNullIsPassed()
        {
            //Given
            var searchTerms = default(string);
            var sut = new EventRepo(_mockApiProvider.Object);

            //When
            await sut.GetEventDetails(searchTerms);

            //Then
            _mockApiProvider.Verify(m => m.GetViagogoApiClient(), Times.Never);
        }

        [Test]
        public async Task ShouldCallTheApiIfSearchTermIsNotNull()
        {
            //Given
            var sut = new EventRepo(_mockApiProvider.Object);
            var searchTerms = "stereophonics";

            //When
            await sut.GetEventDetails(searchTerms);

            //Then
            _mockApiProvider.Verify(m => m.GetViagogoApiClient(), Times.Once);
        }

        [Test]
        public async Task ShouldReturnNullIfNoResultsFromApiCall()
        {
            //Given
            var searchTerms = $"no-results-{Guid.NewGuid()}";

            var sut = new EventRepo(_mockApiProvider.Object);

            //When
            var results = await sut.GetEventDetails(searchTerms);

            //Then
            results.Should().BeNull();
        }

        [Test]
        public async Task ShouldReturnNullIfEventIdIsNotExisting()
        {
            //Given
            var id = Int32.MaxValue;

            var sut = new EventRepo(new ViagogoApiProvider(new CredentialProvider(), "MyTestApp"));

            //When
            var results = await sut.GetEventListings(id);

            results.Should().BeNull();
        }

        [Test]
        public async Task ShouldReturnListingsGivenValidEventId()
        {
            //Given
            var id = 2159132;

            var sut = new EventRepo(new ViagogoApiProvider(new CredentialProvider(), "MyTestApp"));

            //When
            var results = await sut.GetEventListings(id);

            results.Should().NotBeNull();
        }
    }


}
