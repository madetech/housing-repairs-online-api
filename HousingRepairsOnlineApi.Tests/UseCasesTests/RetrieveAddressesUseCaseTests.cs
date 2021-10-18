using System;
using FluentAssertions;
using HousingRepairsOnlineApi.Domain;
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

        [Fact]
        public void DoesNotReturnAnEmptyCollectionOfAddresses()
        {
            const string TestPostcode = "M3 0W";
            addressGatewayMock.Setup(x => x.Search(It.IsAny<String>())).Returns(new Address[]{new Address()});
            var data = sytemUndertest.Execute(postcode: TestPostcode);
            data.Should().NotBeEmpty();
        }

        [Fact]
        public void ThrowsNullExceptionWhenPostcodeIsNull()
        {
            const string TestPostcode = null;
            Action act = () => sytemUndertest.Execute(TestPostcode);
            act.Should().Throw<ArgumentNullException>();
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
