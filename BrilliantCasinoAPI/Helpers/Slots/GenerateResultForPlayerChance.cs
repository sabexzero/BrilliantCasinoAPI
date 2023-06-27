
namespace BrilliantCasinoAPI.Helpers.Slots;
public static class GenerateResultForPlayerChance
{
    public static KeyValuePair<string, int> GenerateResultPairForLowWinChance(double loseChance, Dictionary<string,int> _combinationTable, Random rnd)
    {
        Dictionary<string, int> resultDic = new Dictionary<string, int>(_combinationTable);
        int targetZeroCount = Convert.ToInt32(((loseChance / 100) * 125 - 80) * 10);

        for (int i = 0; i < targetZeroCount; i++)
        {
            resultDic.Add("zaglushka" + i, 0);
        }

        var resultList = resultDic.ToList();
        return resultList[rnd.Next(resultList.Count)];
    }

    public static KeyValuePair<string, int> GenerateResultPairForHighWinChance(double loseChance, Dictionary<string, int> _combinationTable, Random rnd)
    {
        int maxZeroAmount = Convert.ToInt32((loseChance * 125) / 100);

        var resultList = _combinationTable
            .Where(kv => kv.Value != 0)
            .Concat(_combinationTable.Where(kv => kv.Value == 0).Take(maxZeroAmount))
            .ToList();

        return resultList[rnd.Next(resultList.Count)];
    }
}