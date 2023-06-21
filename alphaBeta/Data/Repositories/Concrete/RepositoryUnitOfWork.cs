
using Microsoft.AspNetCore.Identity;

public class RepositoryUnitOfWork : IRepositoryUnitOfWork
{
    private readonly GamesDbContext _context;
    private readonly UserManager<Player> _userManager;
    private IBasePlayerRepository<Player> _playerRepository;
    private IBaseBetRepository<Bet> _betRepository;

    public RepositoryUnitOfWork(GamesDbContext context, UserManager<Player> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IBasePlayerRepository<Player> PlayerRepository => _playerRepository ??= new PlayerRepository(_userManager);

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