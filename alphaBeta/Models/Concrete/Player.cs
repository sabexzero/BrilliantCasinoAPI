/// <summary>
/// Модель игрока
/// </summary>
public class Player : Base
{
    public string Username { get; set; }
    public long Balance { get; set; }
    public double WinChance { get; set; }
    public List<Bet> Bets { get; set; }

    public Player(string username)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Username = username;
        Balance = 0;
        WinChance = 0.36;
        Bets = new List<Bet>();
    }
}