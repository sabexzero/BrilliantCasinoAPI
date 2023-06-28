
using BrilliantCasinoAPI.Models.Concrete;
using BrilliantCasinoAPI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/controller")]
public class BetController : Controller
{
    private readonly IBetsService _betsService;

	public BetController(IBetsService betsService)
	{
        _betsService = betsService;
    }

    [HttpGet("GetBetById")]
    public async Task<ActionResult<Bet>> GetBetById(Guid id)
    {
        var bet = await _betsService.GetBetById(id);
        return Ok(bet);
    }
    [HttpGet("GetBetByUserId")]
    public async Task<ActionResult<IEnumerable<Bet>>> GetBetUserById(string userId)
    {
        var bets = await _betsService.GetBetsByUserId(userId);
        return Ok(bets);
    }
    /*    [HttpPut("UpdateBet")]
        public async Task<ActionResult> UpdateBet(Guid id);*/
    [HttpPut("CreateBet")]
    public async Task<ActionResult<Bet>> CreateBet(string playerId, Games game, long betAmount, long result)
    {
        var newBet = new Bet(playerId,game,betAmount,result);
        await _betsService.CreateBet(newBet);
        await _betsService.SaveChanges();
        return Ok(newBet);
    }
    [HttpDelete("DeleteBet")]
    public async Task<ActionResult> DeleteBet(Guid id)
    {
        await _betsService.DeleteBet(id);
        return Ok();
    }


}