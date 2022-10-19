using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HACT.Dtos;
using HashidsNet;
using HousingRepairsOnline.Authentication.Helpers;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Extensions;

namespace HousingRepairsOnlineApi.Gateways;

public class AppointmentsGateway : IAppointmentsGateway
{
    private readonly string authenticationIdentifier;
    private readonly IHashids hasher;
    private readonly HttpClient httpClient;

    public AppointmentsGateway(HttpClient httpClient, string authenticationIdentifier, IHashids hasher)
    {
        this.httpClient = httpClient;
        this.authenticationIdentifier = authenticationIdentifier;
        this.hasher = hasher;
    }

    public async Task<IEnumerable<Appointment>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"/Appointments/AvailableAppointments?sorCode={sorCode}&locationId={locationId}&fromDate={fromDate}");

        request.SetupJwtAuthentication(httpClient, authenticationIdentifier);

        var response = await httpClient.SendAsync(request);

        var result = Enumerable.Empty<Appointment>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            result = await response.Content.ReadFromJsonAsync<List<Appointment>>();
        }

        return result;
    }

    public async Task BookAppointment(Repair repair)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            "/Appointments/BookAppointment");

        request.Content = new StringContent(
            JsonSerializer.Serialize(repair.ToBookAppointmentRequest(hasher)), Encoding.UTF8,
            "application/json");

        request.SetupJwtAuthentication(httpClient, authenticationIdentifier);

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}
