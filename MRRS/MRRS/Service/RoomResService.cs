using Microsoft.EntityFrameworkCore;
using MRRS.Model;
using System.Xml.Linq;
namespace MRRS.Service;
public class RoomResService
{
    private readonly ReservationDbContext _dbContext;
    public RoomResService(ReservationDbContext dbContext)=>
        _dbContext = dbContext;
    public async Task<List<RoomReservation>> DailySchedule(Guid roomId, string date)
    {
        var roomExists = await _dbContext.Rooms.AnyAsync(r => r.Id == roomId);
        if (!roomExists)
        {
            throw new Exception("Комната с указанным идентификатором не найдена.");
        }
        return await _dbContext.RoomsRes
            .Where(rr => rr.RoomId == roomId && 
                rr.BeginRes.ToString().Substring(8, 2) == date || 
                rr.EndRes.ToString().Substring(8, 2) == date)
            .OrderBy(rr => rr.BeginRes)
            .ToListAsync();
    }
    public async Task Add(DateTime beginRes, DateTime endRes, Guid roomId, Guid userId)
    {
        var roomExists = await _dbContext.Rooms.AnyAsync(r => r.Id == roomId);
        if (!roomExists)
        {
            throw new Exception("Комната с указанным идентификатором не найдена.");
        }
        var userExists = await _dbContext.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
        {
            throw new Exception("Пользователь с указанным идентификатором не найден.");
        }
        var roomEntity = new RoomReservation
        {
            Id = Guid.NewGuid(),
            BeginRes = beginRes,
            EndRes = endRes,
            RoomId = roomId,
            UserId = userId
        };
        await _dbContext.AddAsync(roomEntity);
        await _dbContext.SaveChangesAsync();
    }
    public void Update(Guid id, DateTime beginRes, DateTime endRes, Guid roomId, Guid userId)
    {
        var roomRes = _dbContext.RoomsRes
            .FirstOrDefault(rr => rr.Id == id);
        if (roomRes == null)
        {
            throw new Exception("Бронь с заданным идентификатором не найдена.");
        }
        var roomExists = _dbContext.Rooms
                .FirstOrDefaultAsync(r => r.Id == roomId);
        if (roomExists == null)
        {
            throw new Exception("Комната с указанным идентификатором не найдена.");
        }
        var userExists = _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (userExists == null)
        {
            throw new Exception("Пользователь с указанным идентификатором не найден.");
        }
        roomRes.BeginRes = beginRes;
        roomRes.EndRes = endRes;
        roomRes.UserId = userId;
        roomRes.RoomId = roomId;
        _dbContext.RoomsRes.Update(roomRes);
        _dbContext.SaveChangesAsync();

    }
    public async Task Delete(Guid id)
    {

        var idEx = await _dbContext.RoomsRes.AnyAsync(rr => rr.Id == id);
        if (!idEx)
        {
            throw new Exception("Бронь с заданным идентификатором не найдена.");
        }

        await _dbContext.RoomsRes
            .Where(rr => rr.Id == id)
            .ExecuteDeleteAsync();
        await _dbContext.SaveChangesAsync();
    }
    public async Task<List<RoomReservation>> GetAll()
    {
        return await _dbContext.RoomsRes
            .OrderBy(rr => rr.BeginRes)
            .ToListAsync();
    }
    public async Task<RoomReservation?> GetById(Guid id)
    {
        
        var roomReservation = await _dbContext.RoomsRes
        .FirstOrDefaultAsync(rr => rr.Id == id);
        if (roomReservation == null)
        {
            throw new Exception("Бронь с заданным идентификатором не найдена.");
        }
        return roomReservation;
    }
    public async Task<List<RoomReservation>> GetByPage(int page, int pageSize)
    {
       return await _dbContext.RoomsRes
            .Skip((page-1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
}