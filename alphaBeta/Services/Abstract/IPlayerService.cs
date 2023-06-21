
public interface IPlayersService
{
    Task CreatePlayer(string username, string password);
    Task<bool> DeletePlayer(string id);
    Task<IEnumerable<Player>> GetAllPlayers();
    Task<Player> GetPlayerById(string id);
    Task<Player> GetPlayerByName(string username);
    Task<bool> AddClaimForUser(string id, string claim);
    Task<bool> RemoveClaimForUser(string id, string claim);
    Task<bool> UpdatePlayer(Player entity);
    Task<bool> UpdateBalancePlayer(Player entity, Bet bet);
    Task SaveChanges();
}