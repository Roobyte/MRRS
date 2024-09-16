using MRRS.Model;

namespace MRRS
{
    public class Room
    {
        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<RoomReservation> roomReservations { get; set; } = [];
    }
}
