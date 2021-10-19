using System;
using System.Threading.Tasks;
using HousingRepairsOnlineApi.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HousingRepairsOnlineApi.Controllers
{
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
            // TODO: add tests ensuring that query params are passed to the usecase
            var result = await retrieveAddressesUseCase.Execute(postcode);
            return Ok(result);
        }
    }
}
