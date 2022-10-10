using System.Collections.Generic;
using System.Threading.Tasks;
using HousingRepairsOnlineApi.Domain;

namespace HousingRepairsOnlineApi.Gateways;

public interface IAddressGateway
{
    Task<IEnumerable<Address>> Search(string postcode);
}
