﻿using System;
using System.Threading.Tasks;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Helpers;
using HousingRepairsOnlineApi.UseCases;
using Microsoft.AspNetCore.Mvc;
using Sentry;

namespace HousingRepairsOnlineApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RepairController : ControllerBase
{
    private readonly IAppointmentConfirmationSender appointmentConfirmationSender;
    private readonly IBookAppointmentUseCase bookAppointmentUseCase;
    private readonly IInternalEmailSender internalEmailSender;
    private readonly ISaveRepairRequestUseCase saveRepairRequestUseCase;

    public RepairController(
        ISaveRepairRequestUseCase saveRepairRequestUseCase,
        IInternalEmailSender internalEmailSender,
        IAppointmentConfirmationSender appointmentConfirmationSender,
        IBookAppointmentUseCase bookAppointmentUseCase
    )
    {
        this.saveRepairRequestUseCase = saveRepairRequestUseCase;
        this.internalEmailSender = internalEmailSender;
        this.appointmentConfirmationSender = appointmentConfirmationSender;
        this.bookAppointmentUseCase = bookAppointmentUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> SaveRepair([FromBody] RepairRequest repairRequest)
    {
        try
        {
            var result = await saveRepairRequestUseCase.Execute(repairRequest);
            await bookAppointmentUseCase.Execute(result);
            appointmentConfirmationSender.Execute(result);
            await internalEmailSender.Execute(result);
            return Ok(result.Id);
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(ex);
            return StatusCode(500, ex.Message);
        }
    }
}
