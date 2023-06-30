
namespace BrilliantCasinoAPI.Helpers.Exceptions.Lobby;
public class TheMaximumNumberOfPlayersHasAlreadyBeenReachedException : BaseException
{
	public TheMaximumNumberOfPlayersHasAlreadyBeenReachedException()
	{
		ErrorMessage = "The maximum number of players has already been reached";
    }
}