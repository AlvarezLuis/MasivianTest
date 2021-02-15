using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Roulette.Entities.Models
{
    public class Roulette
    {
        [Required]
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
