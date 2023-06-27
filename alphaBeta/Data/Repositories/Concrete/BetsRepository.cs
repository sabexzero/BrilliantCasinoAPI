using Microsoft.EntityFrameworkCore;

using BrilliantCasinoAPI.Models.Concrete;
using BrilliantCasinoAPI.Data.Repositories.Abstract;
using BrilliantCasinoAPI.Helpers.Exceptions.Bet;
using BrilliantCasinoAPI.Helpers.Exceptions.Player;
using BrilliantCasinoAPI.Helpers.Exceptions;

namespace BrilliantCasinoAPI.Data.Repositories.Concrete;
public class BetsRepository : IBaseBetRepository
{
    private readonly GamesDbContext _context;
    public BetsRepository(GamesDbContext context)
    {
        _context = context;
    }
    public async Task Create(Bet entity)
    {
        var forCreateId = await _context.Set<Bet>().FirstOrDefaultAsync(s => s.Id == entity.Id);
        if (forCreateId == null)
            try
            {
                await _context.Set<Bet>().AddAsync(entity);
            }
            catch (Exception)
            {

                throw new SomethingWrongWithCreatingProcessException();
            } 
        else
            throw new BetAlreadyExistException();
    }

    public async Task Delete(Guid id)
    {
        var deleted = await _context.Set<Bet>().FirstOrDefaultAsync(s => s.Id == id);
        if (deleted == null)
            throw new BetNotFoundException();
        try
        {
            _context.Set<Bet>().Remove(deleted);
        }
        catch (Exception)
        {

            throw new SomethingWrongWithDeletingProcessException();
        }
        
    }

    public async Task<Bet> GetById(Guid id)
    {
        var bet =  await _context.Set<Bet>().FirstOrDefaultAsync(s => s.Id == id);
        if (bet == null)
            throw new BetNotFoundException();
        else
            return bet;
    }

    public async Task<IEnumerable<Bet>> GetAll()
    {
        return await _context.Set<Bet>().ToListAsync();
    }
    public async Task Save()
    {
        try
        {
            await _context.SaveChangesAsync();

        }
        catch (Exception)
        {
            throw new SomethingWrongWithSavingProcessException();
        }
    }

    public async Task Update(Bet updatedEntity)
    {
        try
        {
            _context.Set<Bet>().Update(updatedEntity);
            await Save();
        }
        catch (Exception)
        {
            throw new SomethingWrongWithUpdatingProcessException();
        }
    }

    public async Task<IEnumerable<Bet>> GetByPlayerId (string playerId) 
    {
        var playerById = await _context.Set<Player>().FirstOrDefaultAsync(s => s.Id == playerId);
        if (playerById == null)
            throw new PlayerNotFoundException();
        return await _context.Set<Bet>().Where(s => s.PlayerId == playerId).ToListAsync();
    }

    public async Task<IEnumerable<Bet>> GetByUsername(string username)
    {
        var player = await _context.Set<Player>().FirstOrDefaultAsync(s => s.UserName == username);
        if (player == null)
            throw new PlayerNotFoundException();
        return await _context.Set<Bet>().Where(s => s.PlayerId == player.Id).ToListAsync();
    }
}
