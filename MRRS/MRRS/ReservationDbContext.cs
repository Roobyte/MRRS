using Microsoft.EntityFrameworkCore;
using MRRS.Model;
namespace MRRS;
public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
        :base(options)
    {

    }
    public DbSet<RoomReservation> RoomsRes { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}

