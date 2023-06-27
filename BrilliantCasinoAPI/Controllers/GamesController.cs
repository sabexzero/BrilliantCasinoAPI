using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using BrilliantCasinoAPI.Services.Abstract;

namespace BrilliantCasinoAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly ISlotsService _slotsService;
        private readonly IPlayersService _playerService;
        public GamesController(ISlotsService slotsService, IPlayersService playerService)
        {
            _playerService = playerService;
            _slotsService = slotsService;
        }
        [HttpGet("Slots")]
        public async Task<ActionResult<KeyValuePair<string, int>>> Slots(long amount)
        {
            if (User?.Identity?.IsAuthenticated == false)
                return Unauthorized();

            if (User?.HasClaim(ClaimTypes.Role, "SlotsPlayer") == true)
            {
                var player = await _playerService.GetPlayerByName(User.Identity.Name);
                if (player != null)
                {
                    var result = await _slotsService.PlayGameAsync(player.Id, amount);
                    return Ok(result);
                }
            }
            return Unauthorized();
        }
    }
}
