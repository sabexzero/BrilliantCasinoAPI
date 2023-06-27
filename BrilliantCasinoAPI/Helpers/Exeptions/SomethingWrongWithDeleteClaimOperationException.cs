
namespace BrilliantCasinoAPI.Helpers.Exceptions;
public class SomethingWrongWithDeleteClaimOperationException : BaseException
{
    public SomethingWrongWithDeleteClaimOperationException()
    {
        ErrorMessage = "Something Wrong With deleting claim Process";

    }
}