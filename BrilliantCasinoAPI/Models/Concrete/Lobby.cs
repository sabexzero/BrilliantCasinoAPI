
using BrilliantCasinoAPI.Models.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

public class Lobby
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public int PlayersCount { get; set; }
    public int PlayersAmountToStart { get; set; }
    public int PlayersLimit { get; set; }
    [Column(TypeName = "text")]
    public Games Game { get; set; }
    public string State { get; set; }
}