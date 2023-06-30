
namespace BrilliantCasinoAPI.Helpers.Exceptions.Lobby
{
    public class ThePlayerIsNotFoundInTheLobbyException : BaseException
    {
        public ThePlayerIsNotFoundInTheLobbyException()
        {
            ErrorMessage= $"The player is not found in the lobby";
        }
    }
}
