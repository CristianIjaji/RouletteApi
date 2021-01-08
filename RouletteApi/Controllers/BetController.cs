using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouletteApi.DTO;
using RouletteApi.Entities;
using RouletteApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BetController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public BetController(ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost("bet")]
        public async Task<ActionResult> Bet([FromBody] BetCreateDTO betCreateDTO)
        {
            var rouletteEntiti = context.Roulettes.FirstOrDefault(x => x.Id == betCreateDTO.RouletteId);
            if (rouletteEntiti == null)
            {
                return NotFound();
            }
            if (!rouletteEntiti.IsOpen)
            {
                return BadRequest($"The roulette {betCreateDTO.RouletteId} is closed");
            }
            if (!rouletteEntiti.IsEnable)
            {
                return BadRequest($"The roulette {betCreateDTO.RouletteId} is disabled");
            }
            if (string.IsNullOrEmpty(betCreateDTO.Number.ToString()) && string.IsNullOrEmpty(betCreateDTO.Color))
            {
                return BadRequest("Invalid bet, Color or Number is missing");
            }
            var entiti = mapper.Map<Bet>(betCreateDTO);
            context.Add(entiti);
            await context.SaveChangesAsync();
            return Ok(betCreateDTO);
        }

        [HttpGet("play/{rouletteId:int}")]
        public async Task<ActionResult<List<BetPlayDTO>>> List(int rouletteId, [FromQuery] PaginateDTO paginateDTO)
        {
            var queryable = context.Bets.AsQueryable().Where(x => x.RouletteId == rouletteId);
            await HttpContext.InsertPaginationParams(queryable, paginateDTO.RowsByPage);
            var entities = await queryable.Paginate(paginateDTO).ToListAsync();
            return mapper.Map<List<BetPlayDTO>>(entities);
        }
    }
}
