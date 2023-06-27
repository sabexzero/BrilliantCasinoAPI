using Microsoft.AspNetCore.Identity;
using BrilliantCasinoAPI.Helpers.Auth;
using System.Security.Claims;

using BrilliantCasinoAPI.Services.Abstract;
using BrilliantCasinoAPI.Data.Repositories.Abstract;
using BrilliantCasinoAPI.Models.Concrete;
using BrilliantCasinoAPI.Helpers.Exceptions.Player;
using BrilliantCasinoAPI.Helpers.Exceptions;

namespace BrilliantCasinoAPI.Services.Concrete;
public class PlayersService : IPlayersService
{
    private readonly IBaseBetRepository _betRepository;
    private readonly UserManager<Player> _userManager;
    public PlayersService(IBaseBetRepository betRepository, UserManager<Player> userManager)
    {
        _betRepository = betRepository;
        _userManager = userManager;
    }

    public async Task AddClaimForUser(string id, string claim)
    {
        foreach (PlayerClaims playerClaim in Enum.GetValues(typeof(PlayerClaims)))
        {
            if (playerClaim.ToString() == claim)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    throw new PlayerNotFoundException();
                }
                try
                {
                    var result = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, claim));
                    break;
                }
                catch (Exception)
                {

                    throw new SomethingWrongWithAddClaimOperationException();
                }
            }
        }
    }

    public async Task<Player> CreatePlayer(string username, string password)
    {
        var passwordValidator = new PasswordValidator<Player>();
        var findName = await _userManager.FindByNameAsync(username);
        if (findName != null)
            throw new PlayerNameAlreadyExistException();
        var newPlayer = new Player(username);
        var validateResult = await passwordValidator.ValidateAsync(_userManager,newPlayer, password);
        if (validateResult.Succeeded)
        {
            try
            {
                await _userManager.CreateAsync(newPlayer, password);
            }
            catch (Exception)
            {
                throw new SomethingWrongWithCreatingProcessException();
            }
            var player = await _userManager.FindByNameAsync(username);
            await AddClaimForUser(player.Id, PlayerClaims.User.ToString());
            await AddClaimForUser(player.Id, PlayerClaims.SlotsPlayer.ToString());
        }
        else
            throw new PasswordBadException();
        return newPlayer;
    }

    public async Task DeletePlayer(string id)
    {
        var player = await _userManager.FindByIdAsync(id);
        if (player == null)
            throw new PlayerNotFoundException();
        try
        {
            await _userManager.DeleteAsync(player);
        }
        catch (Exception)
        {

            throw new SomethingWrongWithDeletingProcessException();
        }
    }

    public async Task<IEnumerable<Player>> GetAllPlayers()
    {
        return await _userManager.GetUsersForClaimAsync(new Claim(ClaimTypes.Role, "User"));
    }

    public async Task<Player> GetPlayerById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
    public async Task<Player> GetPlayerByName(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task RemoveClaimForUser(string id, string claimString)
    {
        var user = await GetPlayerById(id);
        if (user == null)
            throw new PlayerNotFoundException();
        var claims = await _userManager.GetClaimsAsync(user);
        var claim = claims.FirstOrDefault(c => c.Value == claimString);
        if (claim == null)
            throw new ClaimNotFoundException();
        var result = await _userManager.RemoveClaimAsync(user, claim);
        if (!result.Succeeded)
            throw new SomethingWrongWithDeleteClaimOperationException();
    }
    public async Task UpdateBalancePlayer(Player entity, Bet bet)
    {
        var amountToChangeBalance = bet.Result < 0 ? bet.Result : bet.Result - bet.BetAmount;
        entity.Balance += amountToChangeBalance;
        entity.Bets.Add(bet);
        await UpdatePlayer(entity);
        
    }

    public async Task UpdatePlayer(Player updatedEntity)
    {
        var existingEntity = await GetPlayerById(updatedEntity.Id);

        if (existingEntity == null)
            throw new PlayerNotFoundException();
        try
        {
            await _userManager.UpdateAsync(updatedEntity);
        }
        catch (Exception)
        {
            throw new SomethingWrongWithUpdatingProcessException();
        }
        
    }
}