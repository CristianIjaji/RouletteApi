using AutoMapper;
using RouletteApi.DTO;
using RouletteApi.Entities;

namespace RouletteApi.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Roulette, RouletteDTO>().ReverseMap();
            CreateMap<RouletteCreateDTO, Roulette>();
            CreateMap<Bet, BetDTO>().ReverseMap();
            CreateMap<BetCreateDTO, Bet>();
            CreateMap<Bet, BetPlayDTO>();
        }
    }
}
