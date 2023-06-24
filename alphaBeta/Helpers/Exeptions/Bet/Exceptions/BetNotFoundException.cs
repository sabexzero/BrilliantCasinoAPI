
public class BetNotFoundException : BaseException
{
	public BetNotFoundException()
	{
		ErrorMessage = "The bet is not in the database";
	}
}