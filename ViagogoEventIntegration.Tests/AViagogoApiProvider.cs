using FluentAssertions;
using NUnit.Framework;
using ViagogoEventIntegration.Infrastruture;

namespace ViagogoEventIntegration.Tests
{
    [TestFixture]
    public class AViagogoApiProvider
    {
        [Test]
        public void ShouldInitANewToken()
        {
            //Given
            var credentialProvider = new CredentialProvider();
            var sut = new ViagogoApiProvider(credentialProvider, "MyApplication");

            //When
            var token = sut.GetToken();

            //Then
            token.Should().NotBe(null);
        }

        [Test]
        public void ShouldReturnAViagogoClient()
        {
            //Given
            var credentialProvider = new CredentialProvider();
            var sut = new ViagogoApiProvider(credentialProvider, "MyApplication");

            //When
            var client = sut.GetViagogoApiClient();

            //Then
            client.Should().NotBe(null);
        }
    }
}