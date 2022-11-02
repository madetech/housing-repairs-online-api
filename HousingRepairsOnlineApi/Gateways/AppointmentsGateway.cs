using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HashidsNet;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Dtos;
using HousingRepairsOnlineApi.Extensions;

namespace HousingRepairsOnlineApi.Gateways;

public class AppointmentsGateway : IAppointmentsGateway
{
    private readonly IHashids hasher;
    private readonly HttpClient httpClient;

    public AppointmentsGateway(HttpClient httpClient, IHashids hasher)
    {
        this.httpClient = httpClient;
        this.hasher = hasher;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"/Appointments/AvailableAppointments?sorCode={sorCode}&locationId={locationId}&fromDate={fromDate}");

        var response = await httpClient.SendAsync(request);

        var result = Enumerable.Empty<AppointmentDto>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            result = await response.Content.ReadFromJsonAsync<List<AppointmentDto>>();
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

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}
