using System;
using System.Collections.Generic;
using System.Globalization;
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
    private readonly string _addressesOrganisationId;

    public AddressGateway(string addressesApiUrl, string addressesOrganisationId)
    {
        _addressesApiUrl = addressesApiUrl;
        _addressesOrganisationId = addressesOrganisationId;
    }

    public async Task<IEnumerable<Address>> Search(string postcode)
    {
        var sanitisedPostcode = postcode
            .Replace(" ", string.Empty,StringComparison.InvariantCultureIgnoreCase)
            .ToUpper(CultureInfo.InvariantCulture);

        var response = await _addressesApiUrl.AppendPathSegment("/addresses").SetQueryParam("postcode", sanitisedPostcode)
            .WithHeader("organisation_id", _addressesOrganisationId)
            .GetAsync()
            .ReceiveJson<List<BuildingsRegisterAddress>>();


        return response.ToAddresses();
    }
}
