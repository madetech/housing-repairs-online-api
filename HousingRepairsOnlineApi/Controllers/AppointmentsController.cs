using System;
using System.Threading.Tasks;
using HousingRepairsOnlineApi.Extensions;
using HousingRepairsOnlineApi.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HousingRepairsOnlineApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IRetrieveAvailableAppointmentsUseCase retrieveAvailableAppointmentsUseCase;
        private readonly ILogger<AppointmentsController> logger;

        public AppointmentsController(
            IRetrieveAvailableAppointmentsUseCase retrieveAvailableAppointmentsUseCase,
            ILogger<AppointmentsController> logger)
        {
            this.retrieveAvailableAppointmentsUseCase = retrieveAvailableAppointmentsUseCase;
            this.logger = logger;
        }

        [HttpGet]
        [Route("AvailableAppointments")]
        public async Task<IActionResult> AvailableAppointments(
            [FromQuery] string repairLocation,
            [FromQuery] string repairProblem,
            [FromQuery] string repairIssue,
            [FromQuery] string locationId,
            [FromQuery] DateTime? fromDate = null)
        {
            try
            {
                var result = await retrieveAvailableAppointmentsUseCase.Execute(repairLocation, repairProblem, repairIssue, locationId, fromDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.ErrorRetrievingAppointments(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
