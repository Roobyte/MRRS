namespace MRRS.Model
{
    public class RoomReservation
    {
        public Guid Id { get; set; }
        public DateTime BeginRes {  get; set; }
        public DateTime EndRes {  get; set; }
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public Room? Room { get; set; }
        public User? User { get; set; }
    }
}
