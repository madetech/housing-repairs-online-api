using System;

namespace HousingRepairsOnlineApi.Dtos;

public record AppointmentDto
{
    public string Id { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}