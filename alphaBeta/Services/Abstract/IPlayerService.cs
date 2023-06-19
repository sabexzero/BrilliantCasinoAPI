
public interface IPlayersService
{
    Task CreatePlayer(Player entity);
    Task<bool> DeletePlayer(Guid id);
    Task<IEnumerable<Player>> GetAllPlayers();
    Task<Player> GetPlayer(Guid id);
    Task<bool> UpdatePlayer(Player entity);
    Task<bool> UpdateBalancePlayer(Player entity, Bet bet);
    Task SaveChanges();
}