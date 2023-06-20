
public class RepositoryUnitOfWork : IRepositoryUnitOfWork
{
    private readonly GamesDbContext _context;
    private IBasePlayerRepository<Player> _playerRepository;
    private IBaseBetRepository<Bet> _betRepository;

    public RepositoryUnitOfWork(GamesDbContext context)
    {
        _context = context;
    }

    public IBasePlayerRepository<Player> PlayerRepository => _playerRepository ??= new PlayerRepository(_context);

    public IBaseBetRepository<Bet> BetRepository => _betRepository ??= new BetsRepository(_context);

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public async void Dispose()
    {
        await _context.DisposeAsync();
    }
}