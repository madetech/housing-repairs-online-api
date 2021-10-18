using FluentAssertions;
using HousingRepairsOnlineApi.Gateways;
using HousingRepairsOnlineApi.UseCases;
using Moq;
using Xunit;

namespace HousingRepairsOnlineApi.Tests.UseCasesTests
{
    public class RetrieveAddressesUseCaseTests
    {
        private readonly RetrieveAddressesUseCase sytemUndertest;
        private readonly Mock<IAddressGateway> addressGatewayMock;

        public RetrieveAddressesUseCaseTests()
        {
            addressGatewayMock = new Mock<IAddressGateway>();
            sytemUndertest = new RetrieveAddressesUseCase(addressGatewayMock.Object);
        }

        [Fact]
        public void ReturnsEmptyWhenNoAddressesAreFound()
        {
            var data = sytemUndertest.Execute("");
            data.Should().BeEmpty();
        }

        [Fact]
        public void GatewayGetsCalledWithPostCode()
        {
            const string TestPostcode = "M3 0W";
            sytemUndertest.Execute(postcode: TestPostcode);
            addressGatewayMock.Verify(x => x.Search(TestPostcode), Times.Once);
        }
    }
}
