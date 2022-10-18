﻿using System;
using System.Threading.Tasks;
using HashidsNet;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Extensions;
using HousingRepairsOnlineApi.Helpers;
using HousingRepairsOnlineApi.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HousingRepairsOnlineApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RepairController : ControllerBase
{
    private readonly IAppointmentConfirmationSender appointmentConfirmationSender;
    private readonly IBookAppointmentUseCase bookAppointmentUseCase;
    private readonly IHashids hasher;
    private readonly ILogger<RepairController> logger;
    private readonly ISaveRepairRequestUseCase saveRepairRequestUseCase;

    public RepairController(
        ILogger<RepairController> logger,
        ISaveRepairRequestUseCase saveRepairRequestUseCase,
        IAppointmentConfirmationSender appointmentConfirmationSender,
        IBookAppointmentUseCase bookAppointmentUseCase,
        IHashids hasher
    )
    {
        this.logger = logger;
        this.saveRepairRequestUseCase = saveRepairRequestUseCase;
        this.appointmentConfirmationSender = appointmentConfirmationSender;
        this.bookAppointmentUseCase = bookAppointmentUseCase;
        this.hasher = hasher;
    }

    [HttpPost]
    public async Task<IActionResult> SaveRepair([FromBody] RepairRequest repairRequest)
    {
        try
        {
            var result = await saveRepairRequestUseCase.Execute(repairRequest);
            await bookAppointmentUseCase.Execute(result);
            appointmentConfirmationSender.Execute(result);

            logger.AfterAddRepair(result.Id);
            return Ok(result.GetReference(hasher));
        }
        catch (Exception ex)
        {
            logger.ErrorSavingRepair(ex);
            return StatusCode(500, ex.Message);
        }
    }
}
