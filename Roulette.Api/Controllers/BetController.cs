using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Roulette.Entities.Interfaces;
using Roulette.Entities.Request;
using Roulette.Entities.Responses;
using Roulette.Entities.Utils;

namespace Roulette.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : GenerateResponse
    {        
        private readonly IBetServices BetServices;

        public BetController(IBetServices betServices)
        {
            BetServices = betServices;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post(BetRequest request)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
            }
            else
            {
                try
                {
                    HttpContext.Request.Headers.TryGetValue("userId", out StringValues userId);
                    if (string.IsNullOrEmpty(userId))
                    {
                        response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                        response.Errors.Add(new Error(response.StatusCode.ToString(), "Usuario nulo o invalido"));
                    }
                    else if((request.Color == 0 && request.Number == null) || (request.Color != 0 && request.Number != null))
                    {
                        response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                        response.Errors.Add(new Error(response.StatusCode.ToString(), "Apuesta invalida, debe apostar por numero o color, en cada apuesta"));
                    }
                    else
                    {
                        request.UserId = userId;
                        response = await BetServices.AddBet(request);
                    }
                }
                catch (Exception ex)
                {
                    response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                    response.Errors.Add(new Error(response.StatusCode.ToString(), ex.Message));
                }
            }            
            
            return GenerateResult(response);
        }
    }
}
