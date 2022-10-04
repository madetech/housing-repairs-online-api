using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HACT.Dtos;
using HousingRepairsOnlineApi.Domain;
using JetBrains.Annotations;

namespace HousingRepairsOnlineApi.Gateways;

public interface IAppointmentsGateway
{
    Task<IEnumerable<Appointment>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null);

    Task BookAppointment([NotNull] Repair repair);
}
