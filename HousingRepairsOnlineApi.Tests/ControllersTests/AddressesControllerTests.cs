using System;
using System.Threading.Tasks;
using FluentAssertions;
using HousingRepairsOnlineApi.Controllers;
using HousingRepairsOnlineApi.UseCases;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace HousingRepairsOnlineApi.Tests;

public class AddressesControllerTests : ControllerTests
{
    private readonly Mock<IRetrieveAddressesUseCase> retrieveAddressesUseCaseMock;
    private readonly AddressesController systemUnderTest;

    public AddressesControllerTests()
    {
        retrieveAddressesUseCaseMock = new Mock<IRetrieveAddressesUseCase>();
        systemUnderTest = new AddressesController(
            retrieveAddressesUseCaseMock.Object,
            NullLogger<AddressesController>.Instance);
    }

    [Fact]
    public async Task TestEndpoint()
    {
        const string Postcode = "M3 0W";
        var result = await systemUnderTest.Addresses(Postcode);

        GetStatusCode(result).Should().Be(200);
        retrieveAddressesUseCaseMock.Verify(x => x.Execute(Postcode), Times.Once);
    }

    [Fact]
    public async Task ReturnsErrorWhenFailsToSave()
    {
        const string Postcode = "M3 0W";

        retrieveAddressesUseCaseMock.Setup(x => x.Execute(It.IsAny<string>())).Throws<Exception>();

        var result = await systemUnderTest.Addresses(Postcode);

        GetStatusCode(result).Should().Be(500);
    }
}
