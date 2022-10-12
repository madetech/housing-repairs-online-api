using System;
using System.Threading.Tasks;
using HousingRepairsOnlineApi.Extensions;
using HousingRepairsOnlineApi.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HousingRepairsOnlineApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IRetrieveAddressesUseCase retrieveAddressesUseCase;
    private readonly ILogger<AddressesController> logger;

    public AddressesController(
        IRetrieveAddressesUseCase retrieveAddressesUseCase,
        ILogger<AddressesController> logger)
    {
        this.retrieveAddressesUseCase = retrieveAddressesUseCase;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Addresses([FromQuery] string postcode)
    {
        try
        {
            var result = await retrieveAddressesUseCase.Execute(postcode);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.ErrorRetrievingAddresses(postcode, ex);
            return StatusCode(500, ex.Message);
        }
    }
}
