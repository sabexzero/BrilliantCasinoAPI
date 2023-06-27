using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using BrilliantCasinoAPI.Services.Abstract;
using BrilliantCasinoAPI.Models.Concrete;

namespace BrilliantCasinoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : Controller
{
    private readonly IPlayersService _playersService;
    private readonly SignInManager<Player> _signInManager;

    public PlayerController(IPlayersService playersService, SignInManager<Player> signInManager)
    {
        _playersService = playersService;
        _signInManager = signInManager;
    }

    [HttpGet("GenerateToken")]
    public string CreateToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("My secret goes here really");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Role, "SlotsPlayer"),
    }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    [HttpPut("CreatePlayer")]
    public async Task<ActionResult> CreateNewPlayer(string username, string password)
    {
        var newUser = await _playersService.CreatePlayer(username, password);
        await _signInManager.SignInAsync(newUser, isPersistent: true);
        return Ok();
    }
    [HttpDelete("DeletePlayer")]
    public async Task<ActionResult> DeletePlayer(string id)
    {
        await _playersService.DeletePlayer(id);
        return Ok();
    }
    [HttpGet("OnePlayerById")]
    public async Task<ActionResult<Player>> GetPlayer(string id)
    {
        return await _playersService.GetPlayerById(id);
    }
    [HttpGet("OnePlayerByName")]
    public async Task<ActionResult<Player>> GetPlayerByName(string username)
    {
        return await _playersService.GetPlayerByName(username);
    }
    [HttpGet("AllPlayers")]
    public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers()
    {
        var list = await _playersService.GetAllPlayers();

        return Json(list);
    }
}