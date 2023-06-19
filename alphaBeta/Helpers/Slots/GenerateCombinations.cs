
public static class GenerateCombinations
{
    public static List<string> Generate(int n, int k)
    {
        List<string> combinations = new List<string>();

        SubGenerate("", n, k, combinations);

        return combinations;
    }

    private static void SubGenerate(string currentCombination, int n, int k, List<string> combinations)
    {
        if (currentCombination.Length == k)
        {
            combinations.Add(currentCombination);
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                char symbol = (char)('A' + i); // Используем символы от 'A' до 'A' + n - 1

                string newCombination = currentCombination + symbol;
                SubGenerate(newCombination, n, k, combinations);
            }
        }
    }
}