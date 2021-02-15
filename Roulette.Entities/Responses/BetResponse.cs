using Roulette.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roulette.Entities.Responses
{
    public class BetResponse : Bet
    {
        public string Color { get; set; }
        public double CashWin { get; set; }
    }
}
