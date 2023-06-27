
namespace BrilliantCasinoAPI.Helpers.Exceptions;
public class SomethingWrongWithDeletingProcessException : BaseException
{
    public SomethingWrongWithDeletingProcessException()
    {
        ErrorMessage = "Something Wrong With Deleting Process";

    }
}