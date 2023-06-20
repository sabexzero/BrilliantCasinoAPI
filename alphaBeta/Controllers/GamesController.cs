using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

namespace alphaBeta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly ISlotsService _slotsService;
        public GamesController(ISlotsService slotsService)
        {
            _slotsService = slotsService;
        }
        [HttpGet("Slots")]
        [Authorize("RequireUserClaim")]
        public async Task<ActionResult<KeyValuePair<string, int>>> Slots(Guid playerId, long amount)
        {
            return await _slotsService.PlayGameAsync(playerId, amount);
        }
    }
}
