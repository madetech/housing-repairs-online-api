using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HousingRepairsOnlineApi.Dtos;
using HousingRepairsOnlineApi.Gateways;
using HousingRepairsOnlineApi.Helpers;
using ApplicationTime = HousingRepairsOnlineApi.Domain.AppointmentTime;

namespace HousingRepairsOnlineApi.UseCases;

public class RetrieveAvailableAppointmentsUseCase : IRetrieveAvailableAppointmentsUseCase
{
    private readonly IAppointmentsGateway appointmentsGateway;
    private readonly ISoREngine sorEngine;

    public RetrieveAvailableAppointmentsUseCase(IAppointmentsGateway appointmentsGateway, ISoREngine sorEngine)
    {
        this.appointmentsGateway = appointmentsGateway;
        this.sorEngine = sorEngine;
    }

    public async Task<List<ApplicationTime>> Execute(string repairLocation, string repairProblem,
        string repairIssue, string locationId, DateTime? fromDate = null)
    {
        Guard.Against.NullOrWhiteSpace(repairLocation, nameof(repairLocation));
        Guard.Against.NullOrWhiteSpace(repairProblem, nameof(repairProblem));
        Guard.Against.NullOrWhiteSpace(locationId, nameof(locationId));
        var repairCode = sorEngine.MapSorCode(repairLocation, repairProblem, repairIssue);


        var result = await appointmentsGateway.GetAvailableAppointments(repairCode, locationId, fromDate);
        var convertedResults = result.Select(ConvertFromAppointmentDto).ToList();

        return convertedResults;

        ApplicationTime ConvertFromAppointmentDto(AppointmentDto appointment)
        {
            return new ApplicationTime
            {
                Id = appointment.Id,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
            };
        }
    }
}
