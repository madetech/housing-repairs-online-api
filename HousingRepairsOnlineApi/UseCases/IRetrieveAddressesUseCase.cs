using System.Collections.Generic;
using HousingRepairsOnlineApi.Domain;

namespace HousingRepairsOnlineApi.UseCases
{
    public interface IRetrieveAddressesUseCase
    {
        IEnumerable<Address> Execute(string postcode);
    }
}
