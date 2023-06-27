/// <summary>
/// Контроллер для работы с играми
/// </summary>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using BrilliantCasinoAPI.Services.Abstract;

namespace BrilliantCasinoAPI.Controllers
{
    [ApiController] //атрибут предназначен для помощи в определении контроллеров веб-API
    [Authorize] //атрибут для функционирования авторизации
    [Route("api/[controller]")] //атрибут для роутинга
    public class GamesController : Controller
    {
        private readonly ISlotsService _slotsService; //сервис для работы слотов
        private readonly IPlayersService _playerService; //сервис для работы с игроками
        public GamesController(ISlotsService slotsService, IPlayersService playerService)
        {
            _playerService = playerService;
            _slotsService = slotsService;
        }
        [HttpGet("Slots")] //гет запрос, чтобы к нему обратиться используем /api/Slots
        public async Task<ActionResult<KeyValuePair<string, int>>> Slots(long amount)
        {
            if (User?.Identity?.IsAuthenticated == false) //если пользователь не аутентифицирован,
                return Unauthorized(); // возвращаем ошибку 401 (ошибка авторизации)

            if (User?.HasClaim(ClaimTypes.Role, "SlotsPlayer") == true) //если пользователь, имеет возможность играть в slots
            {
                var player = await _playerService.GetPlayerByName(User.Identity.Name); //достаем его имя из модели аутентифицированного пользователя
                if (player != null) //если такой найден
                {
                    var result = await _slotsService.PlayGameAsync(player.Id, amount); //начинаем игру
                    return Ok(result);
                }
            }
            //в иных случаях тоже возвращаем ошибку 401
            return Unauthorized();
        }
    }
}
