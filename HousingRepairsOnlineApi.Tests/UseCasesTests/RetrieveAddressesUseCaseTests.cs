using System;
using FluentAssertions;
using HousingRepairsOnlineApi.UseCases;
using Xunit;

namespace HousingRepairsOnlineApi.Tests.UseCasesTests
{
    public class RetrieveAddressesUseCaseTests
    {
        [Fact]
        public void CallsGatewayAndRetrievesAddressData()
        {
            var useCase = new RetrieveAddressesUseCase();
            var data = useCase.Execute();
            data.Should().Equals(Array.Empty<object>());
        }
    }
}
