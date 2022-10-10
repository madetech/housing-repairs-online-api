using System;
using System.Threading.Tasks;
using HousingRepairsOnlineApi.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HousingRepairsOnlineApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IRetrieveAddressesUseCase retrieveAddressesUseCase;

    public AddressesController(IRetrieveAddressesUseCase retrieveAddressesUseCase)
    {
        this.retrieveAddressesUseCase = retrieveAddressesUseCase;
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
            Console.WriteLine(ex);
            return StatusCode(500, ex.Message);
        }
    }
}
