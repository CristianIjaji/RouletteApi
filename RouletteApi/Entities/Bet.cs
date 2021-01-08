using System;
using System.ComponentModel.DataAnnotations;

namespace RouletteApi.Entities
{
    [Serializable]
    public class Bet
    {
        public int Id { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int RouletteId { get; set; }
        [Range(0, 36)]
        public int Number { get; set; }
        public string Color { get; set; }
        [Required]
        [Range(1, 10000)]
        public double Amount { get; set; }
    }
}
