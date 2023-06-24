
public class PasswordBadException : BaseException
{
	public PasswordBadException()
	{
		ErrorMessage = "Your password is bad!";
	}
}