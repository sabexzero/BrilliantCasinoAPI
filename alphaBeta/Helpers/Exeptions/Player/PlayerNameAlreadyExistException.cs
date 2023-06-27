
namespace BrilliantCasinoAPI.Helpers.Exceptions.Player;
public class PlayerNameAlreadyExistException : BaseException
{
    public PlayerNameAlreadyExistException()
    {
        ErrorMessage = "There is already a user with this name";
    }
}