using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Services.Abstract;
public interface IPlayersService
{
    Task<Player> CreatePlayer(string username, string password);
    Task DeletePlayer(string id);
    Task<IEnumerable<Player>> GetAllPlayers();
    Task<Player> GetPlayerById(string id);
    Task<Player> GetPlayerByName(string username);
    Task AddClaimForUser(string id, string claim);
    Task RemoveClaimForUser(string id, string claim);
    Task UpdatePlayer(Player entity);
    Task UpdateBalancePlayer(Player entity, Bet bet);
}