using System.Collections.Generic;
using System.Linq;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Dtos;

namespace HousingRepairsOnlineApi.Extensions;

public static class BuildingsRegisterAddressExtensions
{
    /// <summary>
    ///     To avoid changing the domain, we are throwing away "town_or_city"
    /// </summary>
    public static IEnumerable<Address> ToAddresses(this IEnumerable<BuildingsRegisterAddress> addresses)
    {
        return addresses.Select(address => new Address
        {
            AddressLine1 = address.Line1,
            AddressLine2 = address.Line2,
            PostCode = address.Postcode,
            LocationId = address.Uprns.Find(uprn => uprn.Type == "INTERNAL")?.Value
        });
    }
}
