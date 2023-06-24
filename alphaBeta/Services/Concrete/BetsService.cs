
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

    public async Task<Bet> GetBet(Guid id)
    {
        return await _betRepository.GetById(id);
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