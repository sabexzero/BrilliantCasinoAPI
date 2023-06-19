
public class StringCombintationToCoefficients
{
    public int ConvertToCoefficient(string input)
    {
        char[] characters = input.ToCharArray();

        if (characters[0] == characters[1] && characters[1] == characters[2])
        {
            if (characters[0] == 'A')
            {
                return 100;
            }
            else if (characters[0] == 'B')
            {
                return 10;
            }
            else
            {
                return 7;
            }
        }
        if ((characters[0] == characters[1]) || (characters[1] == characters[2]))
        {
            if (characters[0] == 'A' || characters[1] == 'A')
            {
                return 4;
            }
            else if (characters[0] == 'B' || characters[1] == 'B')
            {
                return 3;
            }
            else
            {
                return 2;
            }
        }

        return 0;
    }
}