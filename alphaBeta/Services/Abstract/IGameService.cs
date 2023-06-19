
public interface IGameService
{
    Task<KeyValuePair<string,int>> PlayGameAsync(Guid IdPlayer, long amount);
}