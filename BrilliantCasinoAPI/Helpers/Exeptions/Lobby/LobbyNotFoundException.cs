
namespace BrilliantCasinoAPI.Helpers.Exceptions.Lobby;
public class LobbyNotFoundException : BaseException
{
	public LobbyNotFoundException()
	{
		ErrorMessage = "Lobby not found in database";
	}
}