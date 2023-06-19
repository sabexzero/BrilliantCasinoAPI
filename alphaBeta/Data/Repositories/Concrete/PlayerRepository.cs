
using Microsoft.EntityFrameworkCore;

public class PlayerRepository : IBaseRepository<Player>
{
    private readonly GamesDbContext _context;
    public PlayerRepository(GamesDbContext context)
    {
        _context = context;
    }
    public async Task Create(Player entity)
    {
        await _context.Set<Player>().AddAsync(entity);
    }

    public async Task<bool> Delete(Guid id)
    {
        var deleted = await _context.Set<Player>().FirstOrDefaultAsync(s => s.Id == id);
        if (deleted != null)
        {
            _context.Set<Player>().Remove(deleted);
            return true;
        }
        return false;
    }

    public async Task<Player> Get(Guid id)
    {
        return await _context.Set<Player>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Player>> GetAll()
    {
        return await _context.Set<Player>().ToListAsync();
    }

    public async Task Update(Player updatedEntity)
    {
        var existingEntity = await Get(updatedEntity.Id);

        existingEntity.Balance = updatedEntity.Balance;
        existingEntity.WinChance = updatedEntity.WinChance;
        existingEntity.Bets = updatedEntity.Bets;
        existingEntity.Username = updatedEntity.Username;
    }
}