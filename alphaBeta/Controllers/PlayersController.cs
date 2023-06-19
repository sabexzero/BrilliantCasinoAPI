using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : Controller
{
    private readonly IPlayersService _playersService;

	public PlayerController(IPlayersService playersService)
	{
		_playersService = playersService;
	}

	[HttpPut("CreatePlayer")]
	public async Task<ActionResult> CreateNewPlayer(string username)
	{
		await _playersService.CreatePlayer(new Player(username));
        await _playersService.SaveChanges();
		return Ok();
	}
	[HttpDelete("DeletePlayer")]
    public async Task<ActionResult> DeletePlayer(Guid id)
    {
        await _playersService.DeletePlayer(id);
        await _playersService.SaveChanges();
        return Ok();
    }
    [HttpGet("OnePlayer")]
    public async Task<ActionResult<Player>> GetPlayer(Guid id)
    {
        return await _playersService.GetPlayer(id);
    }
    [HttpGet("AllPlayers")]
    public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers()
    {
        var list = await _playersService.GetAllPlayers();
        return Json(list);
    }
}