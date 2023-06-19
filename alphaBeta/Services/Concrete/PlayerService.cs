
public class PlayersService : IPlayersService
{
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    public PlayersService(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
    }

    public async Task CreatePlayer(Player entity)
    {
        await _repositoryUnitOfWork.PlayerRepository.Create(entity);
    }

    public async Task<bool> DeletePlayer(Guid id)
    {
        await _repositoryUnitOfWork.PlayerRepository.Delete(id);
        return true;
    }

    public async Task<IEnumerable<Player>> GetAllPlayers()
    {
        return await _repositoryUnitOfWork.PlayerRepository.GetAll();
    }

    public async Task<Player> GetPlayer(Guid id)
    {
        return await _repositoryUnitOfWork.PlayerRepository.Get(id);
    }

    public async Task SaveChanges()
    {
        await _repositoryUnitOfWork.Commit();
    }

    public async Task<bool> UpdateBalancePlayer(Player entity, Bet bet)
    {
        entity.Balance += bet.Result;
        entity.Bets.Add(bet);
        await UpdatePlayer(entity);
        return true;
    }

    public async Task<bool> UpdatePlayer(Player updatedEntity)
    {
        var existingEntity = await GetPlayer(updatedEntity.Id);

        if (existingEntity == null)
        {
            return false; // Возвращаем false, если продукт не найден
        }

        existingEntity.Balance = updatedEntity.Balance;
        existingEntity.WinChance = updatedEntity.WinChance;
        existingEntity.Bets = updatedEntity.Bets;
        existingEntity.Username = updatedEntity.Username;

        return true; // Возвращаем true, если обновление выполнено успешно
    }
}