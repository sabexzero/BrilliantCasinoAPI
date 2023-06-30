/// <summary>
/// Контроллер для работы с игроками
/// </summary>


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using BrilliantCasinoAPI.Services.Abstract;
using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Controllers;

[ApiController] //атрибут предназначен для помощи в определении контроллеров веб-API
[Route("api/[controller]")] //атрибут для роутинга
public class PlayerController : Controller
{
    private readonly IPlayersService _playersService;

    public PlayerController(IPlayersService playersService)
    {
        _playersService = playersService;
    }

    private string CreateToken(string username) //метод для создания токена (пока что недоработан)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); //создаем обработчик токенов
        var key = Encoding.UTF8.GetBytes("My secret goes here really"); //секретный ключ

        var tokenDescriptor = new SecurityTokenDescriptor //создаем дескриптор
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Role, "SlotsPlayer"),
    }), //все нужные клаимсы для токена
            Expires = DateTime.UtcNow.AddDays(7), //срок действия
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //шифруем все это дело
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token); //делаем токен

        return tokenString;
    }

    [HttpPut("CreatePlayer")] //put метод для создания игрока
    public async Task<ActionResult> CreateNewPlayer(string username, string password)
    {
        await _playersService.CreatePlayer(username, password);
        return Ok();
    }
    [HttpDelete("DeletePlayer")] //delete метод для удаления игрока
    public async Task<ActionResult> DeletePlayer(string id)
    {
        await _playersService.DeletePlayer(id);
        return Ok();
    }
    [HttpGet("OnePlayerById")] //get метод на получение игрока по Id
    public async Task<ActionResult<Player>> GetPlayer(string id)
    {
        return await _playersService.GetPlayerById(id);
    }
    [HttpGet("OnePlayerByName")] //get метод на получение игрока по имени
    public async Task<ActionResult<Player>> GetPlayerByName(string username)
    {
        return await _playersService.GetPlayerByName(username);
    }
    [HttpGet("AllPlayers")] //get метод на получение всех игроков
    public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers()
    {
        var list = await _playersService.GetAllPlayers();

        return Json(list);
    }
}