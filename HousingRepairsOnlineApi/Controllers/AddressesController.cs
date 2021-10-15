using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HousingRepairsOnlineApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Addresses()
        {
            return Ok();

        }

    }
}
