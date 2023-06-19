
public interface IBaseRepository<T>
{
    Task Create(T entity);
    Task<T> Get(Guid id);
    Task<IEnumerable<T>> GetAll();
    Task<bool> Delete(Guid id);
    Task Update(T entity);
}