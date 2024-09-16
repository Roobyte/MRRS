using Microsoft.EntityFrameworkCore;
namespace MRRS.Service;
public class RoomService
{
    private readonly ReservationDbContext _dbContext;
    public RoomService(ReservationDbContext dbContext)=>
        _dbContext = dbContext;
    public async Task Create(Guid id, int capacity, string name)
    {
        var roomEntity = new Room
        {
            Id = id,
            Name = name,
            Capacity = capacity
        };
        await _dbContext.AddAsync(roomEntity);
        await _dbContext.SaveChangesAsync();
    }
    public async Task Update(Guid id, int capacity, string name)
    {
        await _dbContext.Rooms
            .Where(r => r.Id == id)
            .ExecuteUpdateAsync(s=>s
            .SetProperty(r=>r.Name,name)
            .SetProperty(r=>r.Capacity,capacity));
    }

    public async Task Delete(Guid id)
    {
        await _dbContext.RoomsRes
            .Where(rr => rr.RoomId == id)
            .ExecuteDeleteAsync();
        await _dbContext.Rooms
            .Where(r => r.Id == id)
            .ExecuteDeleteAsync();
    }
    public async Task<List<Room>> GetAll()
    {
        return await _dbContext.Rooms
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .ToListAsync();
    }
    public async Task<Room?> GetById(Guid Id)
    {
        return await _dbContext.Rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == Id);
    }
}