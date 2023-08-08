namespace Async_Inn.Models
{
    public class HotelDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public List<HotelRoomDTO>? HotelRooms { get; set; }
    }


    public class HotelRoomDTO
    {
        public int HotelID { get; set; }
        public int RoomNumber { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }
        public int RoomID { get; set; }
        public RoomDTO? Room { get; set; }
    }

    public class RoomDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Layout { get; set; }
        public List<AmenityDTO>? Amenities { get; set; }
    }

    public class AmenityDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class RegisterDTO
    {
        public string Username { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserDTO
    {
        public string ID { get; set; }
        public string Username { get; set; }
    }

    public class LoginDTO
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
