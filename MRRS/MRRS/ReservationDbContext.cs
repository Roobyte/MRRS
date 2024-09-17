using Microsoft.EntityFrameworkCore;
using MRRS.Model;
using System.Configuration;
namespace MRRS;
public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
        :base(options)
    {   
            Database.EnsureCreated();
    }
    public DbSet<RoomReservation> RoomsRes { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasData(
             new Room { Id = Guid.NewGuid(), Capacity = 2, Name = "Room A" },
             new Room { Id = Guid.NewGuid(), Capacity = 3, Name = "Room B" },
             new Room { Id = Guid.NewGuid(), Capacity = 4, Name = "Room C" }
         );
        modelBuilder.Entity<User>().HasData(
             new User { Id = Guid.NewGuid(), Tel = "79892919239", UserName = "Nikita" },
             new User { Id = Guid.NewGuid(), Tel = "79893967979", UserName = "Vadim" },
             new User { Id = Guid.NewGuid(), Tel = "+79999999999", UserName = "Alice" }
         );
       
        base.OnModelCreating(modelBuilder);
    }
}

