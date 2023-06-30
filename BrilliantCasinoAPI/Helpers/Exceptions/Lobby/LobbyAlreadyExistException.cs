
namespace BrilliantCasinoAPI.Helpers.Exceptions.Lobby;
public class LobbyAlreadyExistException : BaseException
{
	public LobbyAlreadyExistException()
	{
		ErrorMessage = "Lobby already exist in database";
	}
}