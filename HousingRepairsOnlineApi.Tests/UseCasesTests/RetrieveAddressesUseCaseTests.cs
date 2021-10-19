using System;
using System.Threading.Tasks;
using FluentAssertions;
using HACT.Dtos;
using HousingRepairsOnlineApi.Gateways;
using HousingRepairsOnlineApi.UseCases;
using Moq;
using Xunit;
using Address = HousingRepairsOnlineApi.Domain.Address;

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
        public async Task ReturnsEmptyWhenNoAddressesAreFound()
        {
            var data = await sytemUndertest.Execute("");
            data.Should().BeEmpty();
        }

        [Fact]
        public void GatewayGetsCalledWithPostCode()
        {
            const string TestPostcode = "M3 0W";
            sytemUndertest.Execute(postcode: TestPostcode);
            addressGatewayMock.Verify(x => x.Search(TestPostcode), Times.Once);
        }

        [Fact]
        public async Task DoesNotReturnAnEmptyCollectionOfAddresses()
        {
            const string TestPostcode = "M3 0W";
            addressGatewayMock.Setup(x => x.Search(It.IsAny<string>()))
                .ReturnsAsync(new PropertyAddress[] { new PropertyAddress() });
            var data = await sytemUndertest.Execute(postcode: TestPostcode);
            data.Should().NotBeEmpty();
        }

        [Fact]
        public void ThrowsNullExceptionWhenPostcodeIsNull()
        {
            const string TestPostcode = null;
            Func<Task> act = async () => await sytemUndertest.Execute(TestPostcode);
            act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void DoesNotCallAddressGatewayWhenPostcodeIsEmpty()
        {
            const string TestPostcode = "";
            var data = sytemUndertest.Execute(postcode: TestPostcode);
            addressGatewayMock.Verify(x => x.Search(TestPostcode), Times.Never);
        }
    }
}
