
public class BetsService : IBetsService
{
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    public BetsService(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
    }
    public async Task CreateBet(Bet entity)
    {
        await _repositoryUnitOfWork.BetRepository.Create(entity);
    }

    public async Task<bool> DeleteBet(Guid id)
    {
        await _repositoryUnitOfWork.BetRepository.Delete(id);
        return true;
    }

    public async Task<IEnumerable<Bet>> GetAllBets()
    {
        return await _repositoryUnitOfWork.BetRepository.GetAll();
    }

    public async Task<Bet> GetBet(Guid id)
    {
        return await _repositoryUnitOfWork.BetRepository.GetById(id);
    }

    public async Task SaveChanges()
    {
        await _repositoryUnitOfWork.Commit();
    }

    public async Task<bool> UpdateBet(Bet entity)
    {
        await _repositoryUnitOfWork.BetRepository.Update(entity);
        return true;
    }
}