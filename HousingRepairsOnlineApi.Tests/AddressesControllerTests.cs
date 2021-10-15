using FluentAssertions;
using System.Threading.Tasks;
using HousingRepairsOnlineApi.Controllers;
using Xunit;

namespace HousingRepairsOnlineApi.Tests
{
    public class AddressesControllerTests : ControllerTests
    {
        private AddressesController sytemUndertest;

        public AddressesControllerTests()
        {
            sytemUndertest = new AddressesController();
        }
        [Fact]
        public async Task TestEndpoint()
        {
            var result = await sytemUndertest.Addresses();
            Assert.Equal(true, true);
            GetStatusCode(result).Should().Be(200);

        }
    }
}
