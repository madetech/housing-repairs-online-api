using System.Net.Http;
using HousingRepairsOnlineApi.Gateways;
using Moq;
using RichardSzalay.MockHttp;
using Xunit;

namespace HousingRepairsOnlineApi.Tests.GatewaysTests
{
    public class AddressGatewayTests
    {
        private readonly AddressGateway addressGateway;
        private readonly Mock<HttpClient> httpClientMock;
        private readonly MockHttpMessageHandler mockHttp;

        public AddressGatewayTests()
        {
            mockHttp = new MockHttpMessageHandler();
            addressGateway = new AddressGateway(mockHttp.ToHttpClient());
        }

        [Fact]
        public void ARequestIsMade()
        {
            // Arrange
            const string Postcode = "M3 OW";

            var request = mockHttp.When($"https://our-porxy-UH.api/address?postcode={Postcode}");

            // Act
            _ = addressGateway.Search(Postcode);

            // Assert
            var count = mockHttp.GetMatchCount(request);
            Assert.Equal(1, count);

        }

    }
}
