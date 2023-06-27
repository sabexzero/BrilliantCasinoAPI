
namespace BrilliantCasinoAPI.Helpers.Exceptions;
public class SomethingWrongWithCreatingProcessException : BaseException
{
	public SomethingWrongWithCreatingProcessException()
	{
		ErrorMessage = "Something Wrong With Creating Process";

    }
}