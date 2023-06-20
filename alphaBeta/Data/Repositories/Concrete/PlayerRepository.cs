
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
    public async Task<Player> Create(string username, string password)
    {
        var player = new Player(username);
        var result = await _userManager.CreateAsync(player, password);
        if (result.Succeeded)
        {
            return player;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        var player = await _userManager.FindByIdAsync(id.ToString());
        var result = await _userManager.DeleteAsync(player);
        return result.Succeeded;
    }

    public async Task<Player> GetById(Guid id)
    {
        var player = await _userManager.FindByIdAsync(id.ToString());
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