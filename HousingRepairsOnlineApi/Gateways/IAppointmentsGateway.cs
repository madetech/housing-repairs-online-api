using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Dtos;

namespace HousingRepairsOnlineApi.Gateways;

public interface IAppointmentsGateway
{
    Task<IEnumerable<AppointmentDto>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null);

    Task BookAppointment([NotNull] Repair repair);
}
