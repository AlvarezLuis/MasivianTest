using Roulette.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roulette.Entities.Models
{
    public class Bet
    {
        public int Id { get; set; }
        [Range(0,36,ErrorMessage = "Los numeros disponibles para apuestas son entre 0-36")]
        public int? Number { get; set; }       
        [Range(1,10000, ErrorMessage = "El valor de las apuestas validas son entre 1 y 10.000 USD")]
        public double Value { get; set; }
        [Required]
        public int? RouletteId { get; set; }
        public string UserId { get; set; }
    }
}
