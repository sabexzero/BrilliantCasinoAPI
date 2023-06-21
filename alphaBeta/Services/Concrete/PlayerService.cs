
using Microsoft.AspNetCore.Identity;
using alphaBeta.Helpers.Auth;
using System.Security.Claims;
using System;

public class PlayersService : IPlayersService
{
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly UserManager<Player> _userManager;
    public PlayersService(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
    }

    public async Task<bool> AddClaimForUser(string id, string claim)
    {
        foreach (PlayerClaims playerClaim in Enum.GetValues(typeof(PlayerClaims)))
        {
            if (playerClaim.ToString() == claim)
            {
                var user = await _repositoryUnitOfWork.PlayerRepository.GetById(id);
                if (user == null)
                {
                    // Пользователь не найден, выполните соответствующие действия
                    return false;
                }

                var result = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, claim));
                if (!result.Succeeded)
                {
                    return false;
                }
                break;
            }
        }
        return true;
    }

    public async Task CreatePlayer(string username, string password)
    {
        await _repositoryUnitOfWork.PlayerRepository.Create(username, password);
        var player = _repositoryUnitOfWork.PlayerRepository.GetByUsername(username).Result;
        await AddClaimForUser(player.Id,PlayerClaims.User.ToString());
    }

    public async Task<bool> DeletePlayer(string id)
    {
        await _repositoryUnitOfWork.PlayerRepository.Delete(id);
        return true;
    }

    public async Task<IEnumerable<Player>> GetAllPlayers()
    {
        return await _repositoryUnitOfWork.PlayerRepository.GetAll();
    }

    public async Task<Player> GetPlayerById(string id)
    {
        return await _repositoryUnitOfWork.PlayerRepository.GetById(id);
    }
    public async Task<Player> GetPlayerByName(string username)
    {
        return await _repositoryUnitOfWork.PlayerRepository.GetByUsername(username);
    }

    public async Task<bool> RemoveClaimForUser(string id, string claimString)
    {
        var user = await _repositoryUnitOfWork.PlayerRepository.GetById(id);
        if (user == null)
        {
            // Пользователь не найден, выполните соответствующие действия
            return false;
        }

        var claims = await _userManager.GetClaimsAsync(user);
        var claim = claims.FirstOrDefault(c => c.Value == claimString);
        if (claim == null)
        {
            // Утверждение не найдено, выполните соответствующие действия
            return false;
        }

        var result = await _userManager.RemoveClaimAsync(user, claim);
        if (!result.Succeeded)
        {
            return false;
        }
        return true;
    }

    public async Task SaveChanges()
    {
        await _repositoryUnitOfWork.Commit();
    }

    public async Task<bool> UpdateBalancePlayer(Player entity, Bet bet)
    {
        entity.Balance += bet.Result;
        entity.Bets.Add(bet);
        await UpdatePlayer(entity);
        return true;
    }

    public async Task<bool> UpdatePlayer(Player updatedEntity)
    {
        var existingEntity = await GetPlayerById(updatedEntity.Id);

        if (existingEntity == null)
        {
            return false; // Возвращаем false, если продукт не найден
        }

        await _repositoryUnitOfWork.PlayerRepository.UpdatePlayer(updatedEntity);

        return true; // Возвращаем true, если обновление выполнено успешно
    }
}