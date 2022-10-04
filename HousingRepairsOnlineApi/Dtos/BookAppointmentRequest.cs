using System;

namespace HousingRepairsOnlineApi.Dtos;

public record BookAppointmentRequest
{
    public string Reference { get; init; }
    public string SorCode { get; init; }
    public string LocationId { get; init; }
    public Appointment Appointment { get; init; }
    public ContactDetails ContactDetails { get; init; }
    public string JobDescription { get; init; }
}

public record ContactDetails
{
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string MobileNumber { get; init; }
}

public record Appointment
{
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}
