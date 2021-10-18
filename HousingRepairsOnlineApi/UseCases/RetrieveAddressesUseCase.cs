using System;
using System.Collections.Generic;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Gateways;

namespace HousingRepairsOnlineApi.UseCases
{
    public class RetrieveAddressesUseCase : IRetrieveAddressesUseCase
    {
        private readonly IAddressGateway addressGateway;

        public RetrieveAddressesUseCase(IAddressGateway addressGateway)
        {
            this.addressGateway = addressGateway;
        }

        public IEnumerable<Address> Execute(string postcode)
        {
            return Array.Empty<Address>();
        }
    }
}
