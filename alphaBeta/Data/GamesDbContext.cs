
using Microsoft.EntityFrameworkCore;

public class GamesDbContext : DbContext
{
	public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
	{

	}

	public DbSet<Bet> Bets { get; set; }
	public DbSet<Player> Players { get; set; }
}