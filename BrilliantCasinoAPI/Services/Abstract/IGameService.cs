
namespace BrilliantCasinoAPI.Services.Abstract;
public interface IGameService
{
    Task<KeyValuePair<string,int>> PlayGameAsync(string IdPlayer, long amount);
}