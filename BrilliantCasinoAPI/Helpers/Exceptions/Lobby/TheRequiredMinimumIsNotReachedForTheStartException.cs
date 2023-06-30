namespace BrilliantCasinoAPI.Helpers.Exceptions.Lobby
{
    public class TheRequiredMinimumIsNotReachedForTheStartException : BaseException
    {
        public TheRequiredMinimumIsNotReachedForTheStartException()
        {
            ErrorMessage = "The required minimum is not reached for the start";
        }
    }
}
