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
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost/api/user/*");
            httpClient.SendAsync(request);
            return null;
        }
    }
}
