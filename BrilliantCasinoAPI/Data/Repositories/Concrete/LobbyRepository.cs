using Microsoft.EntityFrameworkCore;
using BrilliantCasinoAPI.Helpers.Exceptions.Player;

using BrilliantCasinoAPI.Data.Repositories.Abstract;
using BrilliantCasinoAPI.Helpers.Exceptions.Bet;
using BrilliantCasinoAPI.Helpers.Exceptions;
using BrilliantCasinoAPI.Models.Concrete;
using BrilliantCasinoAPI.Helpers.Exceptions.Lobby;

namespace BrilliantCasinoAPI.Data.Repositories.Concrete;
public class LobbyRepository : IBaseLobbyRepository
{
    private readonly GamesDbContext _context; //контекст базы данных
    public LobbyRepository(GamesDbContext context)
    {
        _context = context;
    }
    public async Task Create(Lobby entity)
    {
        var forCreateId = await _context.Set<Lobby>().FirstOrDefaultAsync(s => s.Id == entity.Id); //ищем есть ли такое уже лобби по id
        if (forCreateId == null)
            try
            {
                //такого лобби нет, создаем
                await _context.Set<Lobby>().AddAsync(entity);
            }
            catch (Exception)
            {
                //если что то пошло не так, значит что пошло не так
                throw new SomethingWrongWithCreatingProcessException();
            }
        else
            //иначе прокидаем ошибку, что такая ставка уже создана
            throw new LobbyAlreadyExistException();
    }

    public async Task Delete(Guid id)
    {
        var deleted = await _context.Set<Lobby>().FirstOrDefaultAsync(s => s.Id == id);
        if (deleted == null) //нечего удалять, значит ошибка
            throw new LobbyNotFoundException();
        try
        {
            //есть что удалять - удаляем
            _context.Set<Lobby>().Remove(deleted);
        }
        catch (Exception)
        {
            //иначе что то пошло не так
            throw new SomethingWrongWithDeletingProcessException();
        }
    }

    public async Task<IEnumerable<Lobby>> GetAll()
    {
        return await _context.Set<Lobby>().ToListAsync();
    }

    public async Task<Lobby> GetById(Guid id)
    {
        var lobby = await _context.Set<Lobby>().FirstOrDefaultAsync(s => s.Id == id);
        if (lobby == null) //если ставки нет, вернуть нечего
            throw new BetNotFoundException();
        else
            //есть что вернуть
            return lobby;
    }

    public async Task<IEnumerable<Lobby>> GetByUsername(string username)
    {
        var player = await _context.Set<Player>().FirstOrDefaultAsync(s => s.UserName == username);
        if (player == null) //не нашли игрока
            throw new PlayerNotFoundException();
        return await _context.Set<Lobby>().Where(lobby => lobby.Players.Any(player => player.UserName == username)).ToListAsync();
    }

    public async Task Save()
    {
        try
        {
            await _context.SaveChangesAsync();

        }
        catch (Exception)
        {
            //что то пошло не так с сохранением
            throw new SomethingWrongWithSavingProcessException();
        }
    }

    public async Task Update(Lobby updatedLobby)
    {
        try
        {
            //пытаемся обновить сущность
            _context.Set<Lobby>().Update(updatedLobby);
            await Save();
        }
        catch (Exception)
        {
            //что то пошло не так
            throw new SomethingWrongWithUpdatingProcessException();
        }
    }
}