
using BrilliantCasinoAPI.Models.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

public class Lobby
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Active { get; set; }
    public string Title { get; set; }
    public IEnumerable<Player> Players { get; set; }
    public int AmountToStart { get; set; }
    public int PlayersLimit { get; set; }
    [Column(TypeName = "text")]
    public Games Game { get; set; }
    public string State { get; set; }

    public Lobby(string title, int amountToStart, int playersLimit, Games game)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Title = title;
        Players = new List<Player>();
        AmountToStart = amountToStart;
        PlayersLimit = playersLimit;
        Game = game;
        Active = true;
        State = "Preparing to start";

    }
}