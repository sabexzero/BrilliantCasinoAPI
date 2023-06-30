/// <summary>
/// Контроллер для работы с лобби
/// </summary>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using BrilliantCasinoAPI.Services.Abstract;
using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Controllers
{
    [ApiController] //атрибут предназначен для помощи в определении контроллеров веб-API
    [Route("api/[controller]")] //атрибут для роутинга
    public class LobbyController : Controller
    {
        private readonly ILobbyService _lobbyService; //сервис для работы с лобби
        public LobbyController(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        [HttpGet("GetLobbyById")]
        public async Task<ActionResult<Lobby>> GetLobbyById(Guid id)
        {
            var lobby = await _lobbyService.GetLobby(id);
            return Ok(lobby);
        }
        [HttpGet("GetLobbyByUsername")]
        public async Task<ActionResult<IEnumerable<Lobby>>> GetLobbyByUsername(string username)
        {
            var lobby = await _lobbyService.GetLobbyByUsername(username);
            return Ok(lobby);
        }
        [HttpPost("CreateLobby")]
        public async Task<ActionResult> CreateLobby(string title, int amountToStart, int playersLimit, Games game)
        {
            await _lobbyService.CreateLobby(title, amountToStart, playersLimit, game);
            await _lobbyService.SaveChanges();
            return Ok();
        }
        [HttpPut("DeleteLobby")]
        public async Task<ActionResult> DeleteLobby(Guid id)
        {
            await _lobbyService.DeleteLobby(id);
            await _lobbyService.SaveChanges();
            return Ok();
        }
        [HttpGet("GetAllLobby")]
        public async Task<ActionResult<IEnumerable<Lobby>>> GetAllLobby()
        {
            var lobby = await _lobbyService.GetAllLobby();
            return Ok(lobby);
        }
        [HttpPost("RemovePlayerFromLobby")]
        public async Task<ActionResult> RemovePlayerFromLobby(string userId, Guid lobbyId)
        {
            await _lobbyService.RemovePlayerFromLobby(lobbyId, userId);
            await _lobbyService.SaveChanges();
            return Ok();
        }
        [HttpPost("AddPlayerToLobby")]
        public async Task<ActionResult> AddPlayerToLobby(string userId, Guid lobbyId)
        {
            await _lobbyService.AddPlayerToLobby(lobbyId, userId);
            await _lobbyService.SaveChanges();
            return Ok();
        }
        [HttpGet("GetAmountOfPlayersInLobby")]
        public async Task<ActionResult<int>> GetAmountOfPlayersInLobby(Guid id)
        {
            var count = await _lobbyService.GetPlayersAmount(id);
            return Ok(count);
        }

    }
}
