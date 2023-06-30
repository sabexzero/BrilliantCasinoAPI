
using BrilliantCasinoAPI.Data.Repositories.Abstract;
using BrilliantCasinoAPI.Helpers.Exceptions;
using BrilliantCasinoAPI.Helpers.Exceptions.Lobby;
using BrilliantCasinoAPI.Helpers.Exceptions.Player;
using BrilliantCasinoAPI.Models.Concrete;
using BrilliantCasinoAPI.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace BrilliantCasinoAPI.Services.Concrete;
public class LobbyService : ILobbyService
{
    private readonly IBaseLobbyRepository _lobbyRepository;
    private readonly IPlayersService _playerService;
    public LobbyService(IBaseLobbyRepository lobbyRepository, IPlayersService playerService)
    {
        _lobbyRepository = lobbyRepository;
        _playerService = playerService;
    }
    public async Task AddPlayerToLobby(Guid lobbyId, string playerId)
    {
        var player = await _playerService.GetPlayerById(playerId);
        if (player == null)
            throw new PlayerNotFoundException();
        var lobby = await _lobbyRepository.GetById(lobbyId);
        if (player.LobbyKey == lobby.Id.ToString())
            throw new ThePlayerIsAlreadyInTheLobbyException();
        if (lobby.PlayersCount == lobby.PlayersLimit)
            throw new TheMaximumNumberOfPlayersHasAlreadyBeenReachedException();
        try
        {
            player.LobbyKey = lobbyId.ToString();
            lobby.PlayersCount += 1;
            if (lobby.PlayersCount == lobby.PlayersAmountToStart)
                lobby.State = "Ready To Start";
            await _playerService.UpdatePlayer(player);
            await _lobbyRepository.Update(lobby);
        }
        catch (Exception)
        {

            throw new SomethingWrongWithUpdatingProcessException();
        }
    }

    public async Task CreateLobby(string title, int amountToStart, int playersLimit, Games game)
    {
        var newLobby = new Lobby
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            State = "Not enough players to start",
            Title = title,
            PlayersCount = 0,
            PlayersAmountToStart = amountToStart,
            PlayersLimit = playersLimit,
            Game = game
        };
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
        try
        {   
            findDeletingLobby.State = "Closed";
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
        return gettinLobby;
    }
    public async Task<int> GetPlayersAmount(Guid id)
    {
        var lobby = await _lobbyRepository.GetById(id);
        return lobby.PlayersCount;
    }
    public async Task<Lobby> GetLobbyByUsername(string username)
    {
        return await _lobbyRepository.GetByUsername(username);
    }

    public async Task RemovePlayerFromLobby(Guid lobbyId, string playerId)
    {
        var findLobby = await _lobbyRepository.GetById(lobbyId);
        var findPlayer = await _playerService.GetPlayerById(playerId);
        var playerInLobby = findPlayer.LobbyKey == findLobby.Id.ToString();
        if (!playerInLobby)
            throw new ThePlayerIsNotFoundInTheLobbyException();
        if (findPlayer == null)
            throw new PlayerNotFoundException();
        try
        {
            findPlayer.LobbyKey = string.Empty;
            findLobby.PlayersCount -= 1;
            if (findLobby.PlayersCount < findLobby.PlayersAmountToStart)
                findLobby.State = "Not enough players to start";
            await _playerService.UpdatePlayer(findPlayer);
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