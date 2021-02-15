using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roulette.Entities.Interfaces;
using Roulette.Entities.Utils;

namespace Roulette.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Roullete")]
    [ApiController]
    public class RoulleteController : GenerateResponse
    {
        private readonly IRouletteServices RouletteServices;

        public RoulleteController(IRouletteServices rouletteServices)
        {
            RouletteServices = rouletteServices;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return GenerateResult(await RouletteServices.GetRoulettes());
        }


        [HttpPut("Open/{id}")]
        public async Task<IActionResult> Open(int id)
        {
            return GenerateResult(await RouletteServices.OpenRoulette(id));
        }


        [HttpPost("Close/{id}")]
        public async Task<IActionResult> Close(int id)
        {
            return GenerateResult(await RouletteServices.CloseRoulette(id));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateRoullete()
        {
            return GenerateResult(await RouletteServices.AddRoulette());
        }
        
        
    }
}
