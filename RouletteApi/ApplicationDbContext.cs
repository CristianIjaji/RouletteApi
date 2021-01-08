using Microsoft.EntityFrameworkCore;
using RouletteApi.Entities;

namespace RouletteApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Roulette> Roulettes { get; set; }
        public DbSet<Bet> Bets { get; set; }
    }
}
