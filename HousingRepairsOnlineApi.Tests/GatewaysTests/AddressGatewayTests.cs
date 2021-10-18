using System.Net.Http;
using HousingRepairsOnlineApi.Gateways;
using Moq;
using Xunit;

namespace HousingRepairsOnlineApi.Tests.GatewaysTests
{
    public class AddressGatewayTests
    {
        private readonly AddressGateway addressGateway;
        private readonly Mock<HttpClient> httpClientMock;

        public AddressGatewayTests()
        {
            httpClientMock = new Mock<HttpClient>();

            addressGateway = new AddressGateway(httpClientMock.Object);
        }

        [Fact]
        public void ARequestIsMade()
        {
            // Arrange
            const string Postcode = "M3 OW";
            var expectedRequest = new HttpRequestMessage(HttpMethod.Get,
                "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches");

            // Act
            _ = addressGateway.Search(Postcode);

            // Assert
            httpClientMock.Verify(x => x.SendAsync(expectedRequest), Times.Once);
        }

    }
}
