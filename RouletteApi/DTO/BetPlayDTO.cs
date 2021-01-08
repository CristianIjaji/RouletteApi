using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.DTO
{
    public class BetPlayDTO : BetCreateDTO
    {
        public BetPlayDTO()
        {
            Random random = new Random();
            Winner = random.Next(36);
        }
    }
}
