using Microsoft.AspNetCore.Identity;
/// <summary>
/// Модель игрока
/// </summary>
public class Player : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public long Balance { get; set; }
    public double WinChance { get; set; }
    public List<Bet> Bets { get; set; }

    public Player(string username) : base(username)
    {
        CreatedAt = DateTime.UtcNow;
        UserName = username;
        Balance = 0;
        WinChance = 0.36;
        Bets = new List<Bet>();
    }
    public Player() { }
}