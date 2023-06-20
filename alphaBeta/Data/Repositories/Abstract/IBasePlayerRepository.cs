
public interface IBasePlayerRepository<T>
{
    Task<T> Create(string username, string password);
    Task<T> GetById(Guid id);
    Task<T> GetByUsername(string username);
    Task<IEnumerable<T>> GetAll();
    Task<bool> Delete(Guid id);
    Task<bool> UpdatePlayer(Player player);
}