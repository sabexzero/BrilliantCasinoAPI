
namespace BrilliantCasinoAPI.Helpers.Exceptions.Bet;
public class BetNotFoundException : BaseException
{
    public BetNotFoundException()
    {
        ErrorMessage = "The bet is not in the database";
    }
}