using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HACT.Dtos;
using HousingRepairsOnline.Authentication.Helpers;

namespace HousingRepairsOnlineApi.Gateways
{
    public class AddressGateway : IAddressGateway
    {
        private readonly HttpClient httpClient;
        private readonly string addressesApiUrl;
        private string authenticationIdentifier;

        public AddressGateway(HttpClient httpClient, string addressesApiUrl, string authenticationIdentifier)
        {
            this.httpClient = httpClient;
            this.addressesApiUrl = addressesApiUrl;
            this.authenticationIdentifier = authenticationIdentifier;
        }

        public async Task<IEnumerable<PropertyAddress>> Search(string postcode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{addressesApiUrl}/addresses?postcode={postcode}");

            var token = await httpClient.RetrieveAuthenticationToken(addressesApiUrl, authenticationIdentifier);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");

            var response = await httpClient.SendAsync(request);

            var data = new List<PropertyAddress>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                data = await response.Content.ReadFromJsonAsync<List<PropertyAddress>>();
            }

            return data;
        }
    }
}
