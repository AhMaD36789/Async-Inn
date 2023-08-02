namespace Async_Inn.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }

        public List<RoomAmenity>? RoomAminities { get; set; }
    }
}
