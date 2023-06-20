
public interface IBaseBetRepository<T>
{
    Task<T> Create(T entity);
    Task<T> GetById(Guid id);
    Task<IEnumerable<T>> GetByUsername(string username);
    Task<IEnumerable<T>> GetAll();
    Task<bool> Delete(Guid id);
    Task Update(Bet bet);
}