
public interface IPlayersService
{
    Task CreatePlayer(string username, string password);
    Task<bool> DeletePlayer(Guid id);
    Task<IEnumerable<Player>> GetAllPlayers();
    Task<Player> GetPlayerById(Guid id);
    Task<Player> GetPlayerByName(string username);
    Task<bool> AddClaimForUser(Guid id, string claim);
    Task<bool> RemoveClaimForUser(Guid id, string claim);
    Task<bool> UpdatePlayer(Player entity);
    Task<bool> UpdateBalancePlayer(Player entity, Bet bet);
    Task SaveChanges();
}