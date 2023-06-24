
public interface IBetsService
{
    Task CreateBet(Bet entity);
    Task DeleteBet(Guid id);
    Task<IEnumerable<Bet>> GetAllBets();
    Task<Bet> GetBet(Guid id);
    Task UpdateBet(Bet entity);
    Task SaveChanges();
}