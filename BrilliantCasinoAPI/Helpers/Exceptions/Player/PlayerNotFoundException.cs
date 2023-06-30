
namespace BrilliantCasinoAPI.Helpers.Exceptions.Player;
public class PlayerNotFoundException : BaseException
{
    public PlayerNotFoundException()
    {
        ErrorMessage = $"The player with the specified id was not found";
    }
}