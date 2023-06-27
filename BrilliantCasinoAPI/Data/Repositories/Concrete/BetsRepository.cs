/// <summary>
/// Реализация интерфейса репозитория ставок
/// </summary>


using Microsoft.EntityFrameworkCore;

using BrilliantCasinoAPI.Models.Concrete;
using BrilliantCasinoAPI.Data.Repositories.Abstract;
using BrilliantCasinoAPI.Helpers.Exceptions.Bet;
using BrilliantCasinoAPI.Helpers.Exceptions.Player;
using BrilliantCasinoAPI.Helpers.Exceptions;

namespace BrilliantCasinoAPI.Data.Repositories.Concrete;
public class BetsRepository : IBaseBetRepository
{
    private readonly GamesDbContext _context; //контекст базы данных
    public BetsRepository(GamesDbContext context)
    {
        _context = context;
    }
    public async Task Create(Bet entity) //создание ставки
    {
        var forCreateId = await _context.Set<Bet>().FirstOrDefaultAsync(s => s.Id == entity.Id); //ищем есть ли такая уже ставка по id
        if (forCreateId == null)
            try
            {
                //такой ставки нет, создаем
                await _context.Set<Bet>().AddAsync(entity);
            }
            catch (Exception)
            {
                //если что то пошло не так, значит что пошло не так
                throw new SomethingWrongWithCreatingProcessException();
            } 
        else
            //иначе прокидаем ошибку, что такая ставка уже создана
            throw new BetAlreadyExistException();
    }

    public async Task Delete(Guid id) //удаление ставки
    {
        var deleted = await _context.Set<Bet>().FirstOrDefaultAsync(s => s.Id == id);
        if (deleted == null) //нечего удалять, значит ошибка
            throw new BetNotFoundException();
        try
        {
            //есть что удалять - удаляем
            _context.Set<Bet>().Remove(deleted);
        }
        catch (Exception)
        {
            //иначе что то пошло не так
            throw new SomethingWrongWithDeletingProcessException();
        }
        
    }

    public async Task<Bet> GetById(Guid id) //получение ставки по Id
    {
        var bet =  await _context.Set<Bet>().FirstOrDefaultAsync(s => s.Id == id);
        if (bet == null) //если ставки нет, вернуть нечего
            throw new BetNotFoundException();
        else
            //есть что вернуть
            return bet;
    }

    public async Task<IEnumerable<Bet>> GetAll()
    {
        return await _context.Set<Bet>().ToListAsync();
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

    public async Task Update(Bet updatedEntity) //обновление ставки
    {
        try
        {
            //пытаемся обновить сущность
            _context.Set<Bet>().Update(updatedEntity);
            await Save();
        }
        catch (Exception)
        {
            //что то пошло не так
            throw new SomethingWrongWithUpdatingProcessException();
        }
    }

    public async Task<IEnumerable<Bet>> GetByPlayerId (string playerId) //получить все ставки по id игрока
    {
        var playerById = await _context.Set<Player>().FirstOrDefaultAsync(s => s.Id == playerId);
        if (playerById == null) //не нашли игрока
            throw new PlayerNotFoundException();
        return await _context.Set<Bet>().Where(s => s.PlayerId == playerId).ToListAsync(); //вернули список
    }

    public async Task<IEnumerable<Bet>> GetByUsername(string username) //получить все ставки по имени игрока
    {
        var player = await _context.Set<Player>().FirstOrDefaultAsync(s => s.UserName == username);
        if (player == null) //не нашли игрока
            throw new PlayerNotFoundException();
        return await _context.Set<Bet>().Where(s => s.PlayerId == player.Id).ToListAsync(); //вернули список
    }
}
