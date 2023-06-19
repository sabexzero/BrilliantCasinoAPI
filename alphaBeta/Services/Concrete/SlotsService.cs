
public class SlotsService : ISlotsService
{
    private readonly IPlayersService _playersService;
    private readonly IBetsService _betsService;
    private readonly Dictionary<string, int> _combinationTable;
    public SlotsService(IPlayersService playersService, IBetsService betService)
    {
        _playersService = playersService;
        _betsService = betService;
        _combinationTable = new();
        var listOfCombinations = GenerateCombinations.Generate(5, 3);
        StringCombintationToCoefficients StringConverterToCoeffs = new();
        foreach (var item in listOfCombinations)
        {
            _combinationTable.Add(item, StringConverterToCoeffs.ConvertToCoefficient(item));
        }
    }
    public async Task<KeyValuePair<string, int>> PlayGameAsync(Guid IdPlayer, long amount)
    {
        var findPlayer = await _playersService.GetPlayer(IdPlayer);
        double loseChance = 100 - (findPlayer.WinChance * 100);
        KeyValuePair<string, int> resultPair = new();
        Random rnd = new Random();
        if (findPlayer != null)
        {
            resultPair = findPlayer.WinChance <= 0.36 ?
                GenerateResultForPlayerChance.GenerateResultPairForLowWinChance(loseChance, _combinationTable, rnd) :
                GenerateResultForPlayerChance.GenerateResultPairForHighWinChance(loseChance, _combinationTable, rnd);
            long amountForBet = amount * resultPair.Value == 0 ? -amount : amount * resultPair.Value;
            Bet newBet = new(findPlayer.Id, Games.Slots, amount, amountForBet);
            await _playersService.UpdateBalancePlayer(findPlayer, newBet);
            await _betsService.CreateBet(newBet);
            await _playersService.SaveChanges();
        }
        return resultPair;
    }

}