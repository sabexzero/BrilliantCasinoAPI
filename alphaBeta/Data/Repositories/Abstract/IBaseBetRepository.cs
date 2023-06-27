using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Data.Repositories.Abstract;
public interface IBaseBetRepository
{
    Task Create(Bet entity);
    Task<Bet> GetById(Guid id);
    Task<IEnumerable<Bet>> GetByUsername(string username);
    Task<IEnumerable<Bet>> GetAll();
    Task Delete(Guid id);
    Task Save();
    Task Update(Bet bet);
}