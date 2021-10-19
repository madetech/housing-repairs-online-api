using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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
            var addressApiEndpoint = "https://our-porxy-UH.api";
            var addressApiKey = "super secret";
            addressGateway = new AddressGateway(mockHttp.ToHttpClient(), addressApiEndpoint, addressApiKey);
        }

        [Fact]
        public void ARequestIsMade()
        {
            // Arrange
            const string Postcode = "M3 OW";

            mockHttp.Expect($"https://our-porxy-UH.api/address?postcode={Postcode}")
                .WithHeaders("X-API-Key", "super secret");
            // Act
            _ = addressGateway.Search(Postcode);

            // Assert
            mockHttp.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task DataFromApiIsReturned()
        {
            // Arrange
            const string Postcode = "M3 OW";

            mockHttp.Expect($"https://our-porxy-UH.api/address?postcode={Postcode}")
                .WithHeaders("X-API-Key", "super secret")
                .Respond("application/json",
                    "[{ \"UPRN\": \"944225244413\", " +
                    "\"Postbox\": \"null\", " +
                    "\"Room\": \"null\", " +
                    "\"Department\": \"null\", " +
                    "\"Floor\": \"null\", " +
                    "\"Plot\": \"null\", " +
                    "\"BuildingNumber\": \"123\", " +
                    "\"BuildingName\": \"null\", " +
                    "\"ComplexName\": \"null\", " +
                    "\"StreetName\": \"Cute Street\", " +
                    "\"CityName\": \"New Meow City\", " +
                    "\"AddressLine\": [\"123 Cute Street\"], " +
                    "\"Type\": \"null\", " +
                    "\"PostalCode\": \"M3 0W\"}]");
            // Act
            var data = await addressGateway.Search(Postcode);

            // Assert
            mockHttp.VerifyNoOutstandingExpectation();
            // Assert.Equal(, data);
        }

    }
}
