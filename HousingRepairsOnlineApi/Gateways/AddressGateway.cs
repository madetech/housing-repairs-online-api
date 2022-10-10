using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Dtos;
using HousingRepairsOnlineApi.Extensions;

namespace HousingRepairsOnlineApi.Gateways;

public class AddressGateway : IAddressGateway
{
    private readonly string _addressesApiUrl;

    public AddressGateway(string addressesApiUrl)
    {
        _addressesApiUrl = addressesApiUrl;
    }

    public async Task<IEnumerable<Address>> Search(string postcode)
    {
        var response = await _addressesApiUrl.AppendPathSegment("/addresses").SetQueryParam("postcode", postcode)
            .GetAsync()
            .ReceiveJson<List<BuildingsRegisterAddress>>();

        return response.ToAddresses();
    }
}
