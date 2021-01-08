using System;

namespace RouletteApi.Entities
{
    [Serializable]
    public class Roulette
    {
        public int Id { get; set; }
        public bool IsOpen { get; set; }
        public bool IsEnable { get; set; }

        public Roulette()
        {
            IsEnable = true;
        }
    }
}
