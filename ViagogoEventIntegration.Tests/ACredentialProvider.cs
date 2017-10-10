using FluentAssertions;
using NUnit.Framework;
using ViagogoEventIntegration.Abstractions;
using ViagogoEventIntegration.Infrastruture;

namespace ViagogoEventIntegration.Tests
{
    [TestFixture]
    public class ACredentialProvider
    {
        [Test]
        public void ShouldProvideWithAClientId()
        {
            //Given
            ICredentialProvider sut = new CredentialProvider();

            //When
            var clientId = sut.ClientId;

            //Then
            clientId.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void ShouldProvideWithASecretKey()
        {
            //Given
            ICredentialProvider sut = new CredentialProvider();

            //When
            var secretKey = sut.ClientSecret;

            //Then
            secretKey.Should().NotBeNullOrEmpty();
        }
    }
}