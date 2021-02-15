using Roulette.Entities.Interfaces;
using Roulette.Entities.Models;
using Roulette.Entities.Request;
using Roulette.Entities.Responses;
using System;
using System.Net;
using System.Threading.Tasks;
using model = Roulette.Entities.Models;

namespace Roulette.Services
{
    public class BetServices : IBetServices
    {
        private readonly IRouletteAdapter RouletteAdapter;
        private readonly IBetAdapter BetAdapter;

        public BetServices(IRouletteAdapter rouletteAdapter, IBetAdapter betAdapter)
        {
            RouletteAdapter = rouletteAdapter;
            BetAdapter = betAdapter;
        }
        public async Task<Response> AddBet(BetRequest bet)
        {
            Response response = new Response();
            try
            {
                var roulette = await RouletteAdapter.GetRoulette(bet.RouletteId.Value);
                if (roulette != null && roulette.Status)
                {
                    Random rnd = new Random();
                    bet.Id = rnd.Next();
                    await BetAdapter.SetBet(bet);
                    response.StatusCode = HttpStatusCode.OK.GetHashCode();
                    response.Data = bet.GetBetResponse();
                }
                else
                {
                    response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                    response.Errors.Add(new Entities.Utils.Error(response.StatusCode.ToString(), "RouletteId not is valid"));
                }                
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                response.Errors.Add(new Entities.Utils.Error(response.StatusCode.ToString(), ex.Message));
            }
                       
            return response;
        }

        public async Task<Response> GetAllForRoulette(model.Roulette roulette)
        {
            Response response = new Response();
            try
            {
                response.StatusCode = HttpStatusCode.OK.GetHashCode();
                response.Data = await BetAdapter.GetBets(roulette.Id.ToString());
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                response.Errors.Add(new Entities.Utils.Error(response.StatusCode.ToString(), ex.Message));
            }
            return response;
        }
    }
}
