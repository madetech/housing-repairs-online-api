using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using HACT.Dtos;
using HousingRepairsOnlineApi.Domain;

namespace HousingRepairsOnlineApi.Gateways;

public interface IAppointmentsGateway
{
    Task<IEnumerable<Appointment>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null);

    Task BookAppointment([NotNull] Repair repair);
}
