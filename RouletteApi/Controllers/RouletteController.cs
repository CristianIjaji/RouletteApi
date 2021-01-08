using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouletteApi.DTO;
using RouletteApi.Entities;
using RouletteApi.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouletteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouletteController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RouletteController(ApplicationDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<RouletteDTO>>> List([FromQuery] PaginateDTO paginateDTO)
        {
            var queryable = context.Roulettes.AsQueryable();
            await HttpContext.InsertPaginationParams(queryable, paginateDTO.RowsByPage);
            var entities = await queryable.Paginate(paginateDTO).ToListAsync();
            return mapper.Map<List<RouletteDTO>>(entities);
        }

        [HttpGet("list/{id:int}", Name = "getRoulette")]
        public async Task<ActionResult<RouletteDTO>> List(int id)
        {
            var entiti = await context.Roulettes.FirstOrDefaultAsync(x => x.Id == id);
            if (entiti == null)
            {
                return NotFound();
            }
            return mapper.Map<RouletteDTO>(entiti);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] RouletteCreateDTO rouletteCreateDTO)
        {
            var entiti = mapper.Map<Roulette>(rouletteCreateDTO);
            entiti.IsEnable = true;
            context.Add(entiti);
            await context.SaveChangesAsync();
            var rouletteDTO = mapper.Map<RouletteDTO>(entiti);
            return new CreatedAtRouteResult("getRoulette", new { id = rouletteDTO.Id }, rouletteDTO);
        }

        [HttpPost("open/{id:int}")]
        public async Task<ActionResult> Open(int id)
        {
            var entiti = await context.Roulettes.FirstOrDefaultAsync(x => x.Id == id);
            if (entiti.IsOpen)
            {
                return BadRequest($"The roulette {id} is ready open");
            }
            if (!entiti.IsEnable)
            {
                return BadRequest($"The roulette {id} is disabled");
            }
            entiti.IsOpen = true;
            context.Entry(entiti).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
