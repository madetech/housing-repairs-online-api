using System.Collections.Generic;
using HousingRepairsOnlineApi.Domain;

namespace HousingRepairsOnlineApi.UseCases
{
    public interface IRetrieveAddressesUseCase
    {
        List<Address> Execute();
    }
}
