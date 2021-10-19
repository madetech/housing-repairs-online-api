using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HACT.Dtos;

namespace HousingRepairsOnlineApi.Gateways
{
    public class AddressGateway : IAddressGateway
    {
        private readonly HttpClient httpClient;
        private readonly string addressesApiUrl;
        private readonly string addressesApiKey;

        public AddressGateway(HttpClient httpClient, string addressesApiUrl, string  addressesApiKey)
        {
            this.httpClient = httpClient;
            this.addressesApiUrl = addressesApiUrl;
            this.addressesApiKey = addressesApiKey;
        }

        public async Task<IEnumerable<PropertyAddress>> Search(string postcode)
        {
            Console.WriteLine(addressesApiUrl);
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{addressesApiUrl}/address?postcode={postcode}");
            request.Headers.Add("X-API-Key", addressesApiKey);
            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<List<PropertyAddress>>();
        }
    }
}
