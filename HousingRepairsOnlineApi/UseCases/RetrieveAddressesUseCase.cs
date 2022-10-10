using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Gateways;

namespace HousingRepairsOnlineApi.UseCases;

public class RetrieveAddressesUseCase : IRetrieveAddressesUseCase
{
    private readonly IAddressGateway _addressGateway;

    public RetrieveAddressesUseCase(IAddressGateway addressGateway)
    {
        _addressGateway = addressGateway;
    }

    public async Task<IEnumerable<Address>> Execute(string postcode)
    {
        Guard.Against.NullOrWhiteSpace(postcode, nameof(postcode));

        var addresses = await _addressGateway.Search(postcode);
        return addresses;
    }
}
