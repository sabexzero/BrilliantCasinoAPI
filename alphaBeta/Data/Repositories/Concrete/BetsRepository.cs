
using Microsoft.EntityFrameworkCore;
using System;

public class BetsRepository : IBaseBetRepository<Bet>
{
    private readonly GamesDbContext _context;
    public BetsRepository(GamesDbContext context)
    {
        _context = context;
    }
    public async Task<Bet> Create(Bet entity)
    {
        await _context.Set<Bet>().AddAsync(entity);
        return entity;
    }

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

    public async Task<Bet> GetById(Guid id)
    {
        var bet = await _context.Set<Bet>().FirstOrDefaultAsync(s => s.Id == id);
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
        var existingEntity = await GetById(updatedEntity.Id);

        existingEntity.BetAmount = updatedEntity.BetAmount;
        existingEntity.Result = updatedEntity.Result;
    }

    public async Task<IEnumerable<Bet>> GetByPlayerId (string playerId) 
    {
        return await _context.Set<Bet>().Where(s => s.PlayerId == playerId).ToListAsync();
    }

    public async Task<IEnumerable<Bet>> GetByUsername(string username)
    {
        var playerId = _context.Set<Player>().FirstOrDefaultAsync(s => s.UserName == username).Result.Id;
        return await _context.Set<Bet>().Where(s => s.PlayerId == playerId).ToListAsync();
    }
}
