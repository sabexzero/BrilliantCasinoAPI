
public interface IRepositoryUnitOfWork : IDisposable
{
    IBasePlayerRepository<Player> PlayerRepository { get; }
    IBaseBetRepository<Bet> BetRepository { get; }
    Task Commit();
}