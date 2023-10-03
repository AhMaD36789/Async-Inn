namespace Async_Inn.Models
{
    public enum Layout
    {
        Studio,
        OneBedroom,
        TwoBedroom
    }

    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Layout Layout { get; set; }

        public List<RoomAmenity>? RoomAminities { get; set; }
    }
}
