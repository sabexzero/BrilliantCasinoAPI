
namespace BrilliantCasinoAPI.Helpers.Exceptions;
public class ClaimNotFoundException : BaseException
{
	public ClaimNotFoundException()
	{
		ErrorMessage = "Claim not found";
	}
}