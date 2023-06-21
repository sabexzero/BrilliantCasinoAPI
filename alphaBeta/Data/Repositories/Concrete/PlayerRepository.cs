
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

public class PlayerRepository : IBasePlayerRepository<Player>
{
    private readonly UserManager<Player> _userManager;
    public PlayerRepository(UserManager<Player> userManager)
    {
        _userManager = userManager;
    }
    public async Task Create(string username, string password)
    {
        var player = new Player(username);
        await _userManager.CreateAsync(player,password);
    }

    public async Task<bool> Delete(string id)
    {
        var player = await _userManager.FindByIdAsync(id);
        var result = await _userManager.DeleteAsync(player);
        return result.Succeeded;
    }

    public async Task<Player> GetById(string id)
    {
        var player = await _userManager.FindByIdAsync(id);
        return player;
    }
    public async Task<Player> GetByUsername(string username)
    {
        var player = await _userManager.FindByNameAsync(username);
        return player;
    }

    public async Task<IEnumerable<Player>> GetAll()
    {
        var players = await _userManager.Users.ToListAsync();
        return players;
    }

    public async Task<bool> UpdatePlayer(Player player)
    {
        var result = await _userManager.UpdateAsync(player);
        return result.Succeeded;
    }
}