
namespace BrilliantCasinoAPI.Helpers.Exceptions;
public class SomethingWrongWithAddClaimOperationException : BaseException
{
    public SomethingWrongWithAddClaimOperationException()
    {
        ErrorMessage = "Something Wrong with adding claim process";
    }
}