
public interface IBetsService
{
    Task CreateBet(Bet entity);
    Task<bool> DeleteBet(Guid id);
    Task<IEnumerable<Bet>> GetAllBets();
    Task<Bet> GetBet(Guid id);
    Task<bool> UpdateBet(Bet entity);
    Task SaveChanges();
}