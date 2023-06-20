using Microsoft.AspNetCore.Identity;
/// <summary>
/// Модель игрока
/// </summary>
public class Player : IdentityUser
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public long Balance { get; set; }
    public double WinChance { get; set; }
    public List<Bet> Bets { get; set; }

    public Player(string username)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UserName = username;
        Balance = 0;
        WinChance = 0.36;
        Bets = new List<Bet>();
    }
}