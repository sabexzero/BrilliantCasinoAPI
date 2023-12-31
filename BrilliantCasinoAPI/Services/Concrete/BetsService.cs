using BrilliantCasinoAPI.Services.Abstract;
using BrilliantCasinoAPI.Data.Repositories.Abstract;
using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Services.Concrete;
public class BetsService : IBetsService
{
    private readonly IBaseBetRepository _betRepository;
    public BetsService(IBaseBetRepository betRepository)
    {
        _betRepository = betRepository;
    }
    public async Task CreateBet(Bet entity)
    {
        await _betRepository.Create(entity);
    }

    public async Task DeleteBet(Guid id)
    {
        await _betRepository.Delete(id);
    }

    public async Task<IEnumerable<Bet>> GetAllBets()
    {
        return await _betRepository.GetAll();
    }

    public async Task<Bet> GetBetById(Guid id)
    {
        return await _betRepository.GetById(id);
    }

    public async Task<IEnumerable<Bet>> GetBetsByUserId(string userId)
    {
        var listBets = await _betRepository.GetAll();
        return listBets.Where(x => x.PlayerId == userId);
    }

    public async Task SaveChanges()
    {
        await _betRepository.Save();
    }

    public async Task UpdateBet(Bet entity)
    {
        await _betRepository.Update(entity);
    }
}