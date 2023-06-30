
namespace BrilliantCasinoAPI.Helpers.Exceptions.Lobby
{
    public class ThePlayerIsAlreadyInTheLobbyException : BaseException
    {
        public ThePlayerIsAlreadyInTheLobbyException()
        {
            ErrorMessage= $"The player is already in the lobby";
        }
    }
}
