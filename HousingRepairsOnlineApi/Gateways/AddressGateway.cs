using System;
using System.Collections.Generic;
using System.Net.Http;

namespace HousingRepairsOnlineApi.Gateways
{
    public class AddressGateway : IAddressGateway
    {
        private HttpClient httpClient;
        private string addressesApiUrl;
        public AddressGateway(HttpClient httpClient, string addressesApiUrl)
        {
            this.httpClient = httpClient;
            this.addressesApiUrl = addressesApiUrl;
        }

        public IEnumerable<object> Search(string postcode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{addressesApiUrl}/address?postcode={postcode}");
            httpClient.SendAsync(request);
            return null;
        }
    }
}
