using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Services.Abstract;
public interface ILobbyService
{
    Task CreateLobby(string title, int amountToStart, int playersLimit, Games game);
    Task DeleteLobby(Guid id);
    Task<IEnumerable<Lobby>> GetAllLobby();
    Task<Lobby> GetLobbyByUsername(string username);
    Task AddPlayerToLobby(Guid lobbyId, string playerId);
    Task RemovePlayerFromLobby(Guid lobbyId, string playerId);
    Task<Lobby> GetLobby(Guid id);
    Task UpdateLobby(Lobby entity);
    Task SaveChanges();
    Task<int> GetPlayersAmount(Guid id);
}