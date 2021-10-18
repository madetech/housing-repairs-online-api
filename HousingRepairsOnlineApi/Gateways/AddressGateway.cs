using System.Collections.Generic;
using System.Net.Http;

namespace HousingRepairsOnlineApi.Gateways
{
    public class AddressGateway : IAddressGateway
    {
        private HttpClient httpClient;
        public AddressGateway(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public IEnumerable<object> Search(string postcode)
        {
            throw new System.NotImplementedException();
        }
    }
}
