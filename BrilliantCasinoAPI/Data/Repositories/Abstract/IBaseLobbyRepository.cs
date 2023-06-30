/// <summary>
/// Интерфейс репозитория ставок
/// </summary>


using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Data.Repositories.Abstract;
public interface IBaseLobbyRepository
{
    Task Create(Lobby entity);
    Task<Lobby> GetById(Guid id);
    Task<Lobby> GetByUsername(string username);
    Task<IEnumerable<Lobby>> GetAll();
    Task Delete(Guid id);
    Task Save();
    Task Update(Lobby lobby);
}