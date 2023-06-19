
public interface IRepositoryUnitOfWork : IDisposable
{
    IBaseRepository<Player> PlayerRepository { get; }
    IBaseRepository<Bet> BetRepository { get; }
    Task Commit();
}