using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// Model stavki
/// </summary>
public class Bet
{ 
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PlayerId { get; set; }
    [Column(TypeName = "text")]
    public Games Game { get; set; }
    public long BetAmount { get; set; }
    public long Result { get; set; }

    public Bet(string playerId, Games game, long betAmount, long result)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        PlayerId = playerId;
        Game = game;
        BetAmount = betAmount;
        Result = result;
    }
}