using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.DTO
{
    public class BetCreateDTO
    {
        [Required]
        public int RouletteId { get; set; }
        [Range(0, 36)]
        public int? Number { get; set; }
        public string? Color { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Amount { get; set; }
        public int? Winner { get; set; }
    }
}
