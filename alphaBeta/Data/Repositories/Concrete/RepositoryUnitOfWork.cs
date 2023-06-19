
public class RepositoryUnitOfWork : IRepositoryUnitOfWork
{
    private readonly GamesDbContext _context;
    private IBaseRepository<Player> _playerRepository;
    private IBaseRepository<Bet> _betRepository;

    public RepositoryUnitOfWork(GamesDbContext context)
    {
        _context = context;
    }

    public IBaseRepository<Player> PlayerRepository => _playerRepository ??= new PlayerRepository(_context);

    public IBaseRepository<Bet> BetRepository => _betRepository ??= new BetsRepository(_context);

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public async void Dispose()
    {
        await _context.DisposeAsync();
    }
}