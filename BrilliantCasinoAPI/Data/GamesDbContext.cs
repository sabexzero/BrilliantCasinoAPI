/// <summary>
/// Реализация DbContext для базы данных
/// </summary>


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Data;
public class GamesDbContext : IdentityDbContext<Player, IdentityRole, string> //наследуем контекст от Identity, где у нас будут таблицы с пользователями их хешированными паролями
    //и другими нужными таблицами
{
    public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
    {

    }

    public DbSet<Bet> Bets { get; set; } //таблица ставок

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Player>()
            .HasIndex(p => p.Email) //емейл должен быть уникальным
            .IsUnique();
    }
}