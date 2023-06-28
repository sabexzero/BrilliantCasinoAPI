using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Services.Abstract;
public interface IBetsService
{
    Task CreateBet(Bet entity);
    Task DeleteBet(Guid id);
    Task<IEnumerable<Bet>> GetAllBets();
    Task<Bet> GetBetById(Guid id);
    Task<IEnumerable<Bet>> GetBetsByUserId(string username);
    Task UpdateBet(Bet entity);
    Task SaveChanges();
}