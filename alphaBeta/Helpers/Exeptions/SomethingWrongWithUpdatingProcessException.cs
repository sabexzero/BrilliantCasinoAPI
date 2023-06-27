
namespace BrilliantCasinoAPI.Helpers.Exceptions;
public class SomethingWrongWithUpdatingProcessException : BaseException
{
    public SomethingWrongWithUpdatingProcessException()
    {
        ErrorMessage = "Something Wrong With Updating Process";
    }
}