using MRRS.Model;

namespace MRRS
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public List<RoomReservation> resRooms { get; set; } = [];
    }
}
