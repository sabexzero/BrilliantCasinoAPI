
using Microsoft.EntityFrameworkCore;
using System;

public class BetsRepository : IBaseRepository<Bet>
{
    private readonly GamesDbContext _context;
    public BetsRepository(GamesDbContext context)
    {
        _context = context;
    }

    public async Task Create(Bet entity)
    {
        await _context.Set<Bet>().AddAsync(entity);    }

    public async Task<bool> Delete(Guid id)
    {
        var deleted = await _context.Set<Bet>().FirstOrDefaultAsync(s => s.Id == id);
        if (deleted != null)
        {
            _context.Set<Bet>().Remove(deleted);
            return true;
        }
        return false;
    }

    public async Task<Bet> Get(Guid id)
    {
        var bet = await Get(id);
        if (bet != null)
            return bet;
        else
            return null;
    }

    public async Task<IEnumerable<Bet>> GetAll()
    {
        return await _context.Set<Bet>().ToListAsync();
    }

    public async Task Update(Bet updatedEntity)
    {
        var existingEntity = await Get(updatedEntity.Id);

        existingEntity.BetAmount = updatedEntity.BetAmount;
        existingEntity.Result = updatedEntity.Result;
    }

    public async Task<IEnumerable<Bet>> GetByPlayerId (Guid playerId) 
    {
        return await _context.Set<Bet>().Where(s => s.PlayerId == playerId).ToListAsync();
    }
}
