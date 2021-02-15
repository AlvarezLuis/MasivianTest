
using Roulette.Entities.Interfaces;
using Roulette.Entities.Responses;
using model = Roulette.Entities.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Roulette.Entities.Request;
using System.Linq;
using Roulette.Entities.Enum;

namespace Roulette.Services
{
    public class RouletteServices : IRouletteServices
    {
        private readonly IRouletteAdapter RouletteAdapter;
        private readonly IBetServices BetServices;
        public RouletteServices(IRouletteAdapter rouletteAdapter, IBetServices betServices)
        {
            RouletteAdapter = rouletteAdapter;
            BetServices = betServices;
        }

        public async Task<Response> AddRoulette()
        {
            Response response = new Response();
            try
            {
                Random rnd = new Random();
                model.Roulette roulette = new model.Roulette() { Id = rnd.Next(), Status = false };
                await RouletteAdapter.SetRoulette(roulette);
                response.StatusCode = HttpStatusCode.OK.GetHashCode();
                response.Data = roulette;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                response.Errors.Add(new Entities.Utils.Error(response.StatusCode.ToString(), ex.Message));
            }
            return response;
        }

        public async Task<Response> CloseRoulette(int id)
        {
            Response response = new Response();
            try
            {
                var roulette = await RouletteAdapter.GetRoulette(id);
                if (roulette != null && roulette.Status)
                {
                    roulette.Status = false;
                    var listWinners = await GetBetWinners(roulette);
                    if (listWinners.Any())
                    {
                        response.StatusCode = HttpStatusCode.OK.GetHashCode();
                        response.Data = listWinners;
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.OK.GetHashCode();
                        response.Data = "No hay ganadores";
                    }
                    await RouletteAdapter.SetRoulette(roulette);                    
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                    response.Data = "La ruleta esta cerrada o no existe";
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                response.Errors.Add(new Entities.Utils.Error(response.StatusCode.ToString(), ex.Message));
            }
            return response;
        }

        private async Task<List<BetResponse>> GetBetWinners(model.Roulette roulette)
        {
            List<BetResponse> listWin = new List<BetResponse>();
            var response = await BetServices.GetAllForRoulette(roulette);
            if(response.StatusCode == HttpStatusCode.OK.GetHashCode())
            {
                var listBet = (List<BetRequest>)response.Data;
                //Random rnd = new Random();
                var numWin = 10;//rnd.Next(0, 36);                
                foreach (var bet in listBet.Where(x=> x.Number != null))
                {
                    if(bet.Number == numWin)
                    {
                        var result = bet.GetBetResponse();
                        result.CashWin = result.Value * 5;
                        listWin.Add(result);
                    }
                }
                var color = numWin % 2 == 0 ? Color.Red : Color.Black;
                foreach (var bet in listBet.Where(x => x.Color != 0))
                {
                    if (bet.Color == color)
                    {
                        var result = bet.GetBetResponse();
                        result.CashWin = result.Value * 1.8;
                        listWin.Add(result);
                    }
                }

            }
            return listWin;
        }

        public async Task<Response> GetRoulette(int id)
        {
            Response response = new Response();
            try
            {
                var roulette = await RouletteAdapter.GetRoulette(id);
                if (roulette != null)
                {
                    response.StatusCode = HttpStatusCode.OK.GetHashCode();
                    response.Data = roulette;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                }
                
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                response.Errors.Add(new Entities.Utils.Error(response.StatusCode.ToString(), ex.Message));
            }
            return response;
        }

        public async Task<Response> GetRoulettes()
        {
            Response response = new Response();
            try
            {
                response.StatusCode = HttpStatusCode.OK.GetHashCode();
                response.Data = await RouletteAdapter.GetRoulettes();
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                response.Errors.Add(new Entities.Utils.Error(response.StatusCode.ToString(), ex.Message));
            }
            return response;
        }

        public async Task<Response> OpenRoulette(int id)
        {
            Response response = new Response();
            try
            {                
                var roulette = await RouletteAdapter.GetRoulette(id);
                if (roulette != null)
                {
                    var bets = await BetServices.GetAllForRoulette(roulette);
                    if (response.StatusCode == HttpStatusCode.OK.GetHashCode() && ((List<BetRequest>)response.Data).Any())
                    {
                        response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                        response.Data = "denegada";
                    }
                    else
                    {
                        roulette.Status = true;
                        await RouletteAdapter.SetRoulette(roulette);
                        response.StatusCode = HttpStatusCode.OK.GetHashCode();
                        response.Data = "exitosa";
                    }
                    
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                    response.Data = "denegada";
                }

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
