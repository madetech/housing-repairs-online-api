using System;
using System.Collections.Generic;
using FluentAssertions;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.UseCases;
using Xunit;

namespace HousingRepairsOnlineApi.Tests.UseCasesTests
{
    public class RetrieveAddressesUseCaseTests
    {
        [Fact]
        public void RetrievesAddressData()
        {
            var useCase = new RetrieveAddressesUseCase();
            var data = useCase.Execute();
            Assert.IsType<List<Address>>(data);
        }
    }
}
