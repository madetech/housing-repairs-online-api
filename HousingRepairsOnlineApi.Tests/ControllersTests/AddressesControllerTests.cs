using System.Threading.Tasks;
using FluentAssertions;
using HousingRepairsOnlineApi.Controllers;
using HousingRepairsOnlineApi.UseCases;
using Moq;
using Xunit;

namespace HousingRepairsOnlineApi.Tests
{
    public class AddressesControllerTests : ControllerTests
    {
        private AddressesController sytemUndertest;
        private Mock<IRetrieveAddressesUseCase> retrieveAddressesUseCaseMock;

        public AddressesControllerTests()
        {
            retrieveAddressesUseCaseMock = new Mock<IRetrieveAddressesUseCase>();
            sytemUndertest = new AddressesController(retrieveAddressesUseCaseMock.Object);
        }

        [Fact]
        public async Task TestEndpoint()
        {
            var result = await sytemUndertest.Addresses();

            GetStatusCode(result).Should().Be(200);
            retrieveAddressesUseCaseMock.Verify(x => x.Execute(""), Times.Once);
        }
    }
}
