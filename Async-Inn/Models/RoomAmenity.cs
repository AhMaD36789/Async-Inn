namespace Async_Inn.Models
{
    public class RoomAmenity
    {
        public int AmenityID { get; set; }
        public int RoomID { get; set; }

        public Room room { get; set; }

        public Amenity amenity { get; set; }
    }
}
