
public interface IBasePlayerRepository<T>
{
    Task Create(string username, string password);
    Task<T> GetById(string id);
    Task<T> GetByUsername(string username);
    Task<IEnumerable<T>> GetAll();
    Task<bool> Delete(string id);
    Task<bool> UpdatePlayer(Player player);
}