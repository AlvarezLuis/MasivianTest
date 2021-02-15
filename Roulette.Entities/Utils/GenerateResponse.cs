using Microsoft.AspNetCore.Mvc;
using Roulette.Entities.Responses;
using System.Net;

namespace Roulette.Entities.Utils
{
    public class GenerateResponse : Controller
    {
        protected IActionResult GenerateResult<TObject>(TObject response) where TObject : Response
        {
            if (response.StatusCode != HttpStatusCode.OK.GetHashCode())
            {
                foreach (var error in response.Errors)
                {
                    //Write in log
                }
            }
            return this.StatusCode(response.StatusCode, response);
        }
    }
}
