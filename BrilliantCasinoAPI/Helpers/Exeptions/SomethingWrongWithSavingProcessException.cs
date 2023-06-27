
namespace BrilliantCasinoAPI.Helpers.Exceptions;
public class SomethingWrongWithSavingProcessException : BaseException
{
    public SomethingWrongWithSavingProcessException()
    {
        ErrorMessage = "Something Wrong With Saving Process";

    }
}