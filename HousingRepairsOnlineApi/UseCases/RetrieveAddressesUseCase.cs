using System;
using System.Collections.Generic;
using HousingRepairsOnlineApi.Domain;

namespace HousingRepairsOnlineApi.UseCases
{
    public class RetrieveAddressesUseCase: IRetrieveAddressesUseCase
    {
        public List<Address> Execute()
        {
            var address = new Address()
            {
                Uprn = "uprn",
                AddressLine1 = "address line 1",
                AddressLine2 = "address line 2",
                PostCode = "postcode"
            };
            var addresses = new List<Address>();
            addresses.Add(address);
            return addresses;
        }
    }
}
