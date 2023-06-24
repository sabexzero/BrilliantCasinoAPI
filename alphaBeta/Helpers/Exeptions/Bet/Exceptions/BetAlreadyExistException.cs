
public class BetAlreadyExistException : BaseException
{
	public BetAlreadyExistException()
	{
		ErrorMessage = "The essence of the bet is already in the database";

    }
}