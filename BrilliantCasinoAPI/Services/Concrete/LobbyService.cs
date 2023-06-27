
using BrilliantCasinoAPI.Data.Repositories.Abstract;
using BrilliantCasinoAPI.Helpers.Exceptions;
using BrilliantCasinoAPI.Helpers.Exceptions.Lobby;
using BrilliantCasinoAPI.Helpers.Exceptions.Player;
using BrilliantCasinoAPI.Models.Concrete;
using BrilliantCasinoAPI.Services.Abstract;
using Microsoft.AspNetCore.Identity;

public class LobbyService : ILobbyService
{
    private readonly IBaseLobbyRepository _lobbyRepository;
    private readonly UserManager<Player> _userManager;
    public LobbyService(IBaseLobbyRepository lobbyRepository, UserManager<Player> userManager)
    {
        _lobbyRepository = lobbyRepository;
        _userManager = userManager;
    }
    public async Task AddPlayerToLobby(Guid lobbyId, string playerId)
    {
        var player = await _userManager.FindByIdAsync(playerId);
        if (player == null)
            throw new PlayerNotFoundException();
        var lobby = await _lobbyRepository.GetById(lobbyId);
        if(lobby == null)
            throw new LobbyNotFoundException();
        try
        {
            lobby.Players = lobby.Players.Append(player);
            await _lobbyRepository.Update(lobby);
        }
        catch (Exception)
        {

            throw new SomethingWrongWithUpdatingProcessException();
        }
    }

    public async Task CreateLobby(string title, int amountToStart, int playersLimit, Games game)
    {
        var newLobby = new Lobby(title, amountToStart, playersLimit, game);
        var findDuplicate = await _lobbyRepository.GetById(newLobby.Id);
        if (findDuplicate != null)
            throw new LobbyAlreadyExistException();
        try
        {
            await _lobbyRepository.Create(newLobby);
        }
        catch (Exception)
        {

            throw new SomethingWrongWithCreatingProcessException();
        }
    }

    public async Task DeleteLobby(Guid id)
    {
        var findDeletingLobby = await _lobbyRepository.GetById(id);
        if (findDeletingLobby == null)
            throw new LobbyNotFoundException();
        try
        {
            findDeletingLobby.Active = false;
            await _lobbyRepository.Update(findDeletingLobby);
        }
        catch (Exception)
        {
            throw new SomethingWrongWithDeletingProcessException();
        }
    }

    public async Task<IEnumerable<Lobby>> GetAllLobby()
    {
        return await _lobbyRepository.GetAll();
    }

    public async Task<Lobby> GetLobby(Guid id)
    {
        var gettinLobby = await _lobbyRepository.GetById(id);
        if(gettinLobby == null)
            throw new LobbyNotFoundException();
        return gettinLobby;
    }

    public async Task<IEnumerable<Lobby>> GetLobbyByUsername(string username)
    {
        return await _lobbyRepository.GetByUsername(username);
    }

    public async Task RemovePlayerFromLobby(Guid lobbyId, string playerId)
    {
        var findLobby = await _lobbyRepository.GetById(lobbyId);
        if (findLobby == null)
            throw new LobbyNotFoundException();
        var findPlayer = await _userManager.FindByIdAsync(playerId);
        if (findPlayer == null)
            throw new PlayerNotFoundException();
        try
        {
            findLobby.Players.ToList().RemoveAll(player => player.Id == playerId);
            await _lobbyRepository.Update(findLobby);
        }
        catch (Exception)
        {
            throw new SomethingWrongWithUpdatingProcessException();
        }
        
    }

    public async Task SaveChanges()
    {
        try
        {
            await _lobbyRepository.Save();
        }
        catch (Exception)
        {

            throw new SomethingWrongWithSavingProcessException();
        }
    }

    public async Task UpdateLobby(Lobby entity)
    {
        try
        {
            await _lobbyRepository.Update(entity);
        }
        catch (Exception)
        {

            throw new SomethingWrongWithUpdatingProcessException();
        }
    }
}