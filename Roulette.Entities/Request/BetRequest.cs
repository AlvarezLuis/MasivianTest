using Roulette.Entities.Enum;
using Roulette.Entities.Models;
using Roulette.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roulette.Entities.Request
{
    public class BetRequest : Bet
    {        
        public Color Color { get; set; }

        public BetResponse GetBetResponse()
        {
            var response = new BetResponse()
            {
                Id = this.Id,
                Number = this.Number,
                RouletteId = this.RouletteId,
                Value = this.Value,
                Color = this.Color == 0 ? string.Empty : this.Color.ToString("g"),
                UserId = this.UserId
            };

            return response;
        }
    }
}
