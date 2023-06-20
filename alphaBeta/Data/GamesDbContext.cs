using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class GamesDbContext : IdentityDbContext<Player, IdentityRole, string>
{
    public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
    {

    }

    public DbSet<Bet> Bets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Player>()
            .HasIndex(p => p.Email)
            .IsUnique();
    }
}