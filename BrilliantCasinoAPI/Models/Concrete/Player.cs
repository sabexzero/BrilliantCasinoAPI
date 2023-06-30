using Microsoft.AspNetCore.Identity;
/// <summary>
/// Модель игрока
/// </summary>

namespace BrilliantCasinoAPI.Models.Concrete;
public class Player : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public long Balance { get; set; }
    public double WinChance { get; set; }
    public string LobbyKey { get; set; }
    public List<Bet> Bets { get; set; }
    public Player(string username) : base(username)
    {
        CreatedAt = DateTime.UtcNow;
        UserName = username;
        Balance = 1000;
        WinChance = 0.36;
        Bets = new List<Bet>();
        LobbyKey = string.Empty;
    }
    public Player() 
    {
        CreatedAt = DateTime.UtcNow;
        Balance = 1000;
        WinChance = 0.36;
        Bets = new List<Bet>();
        LobbyKey = string.Empty;
    }
}