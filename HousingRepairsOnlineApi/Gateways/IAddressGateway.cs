using System.Collections.Generic;

namespace HousingRepairsOnlineApi.Gateways
{
    public interface IAddressGateway
    {
        IEnumerable<object> Search(string postcode);
    }
}
