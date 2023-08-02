namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoom hotelroom, int HotelID);

        // GET All
        Task<List<HotelRoom>> GetHotelRooms();

        // GET Hotel By Id

        Task<HotelRoom> GetHotelRoom(int HotelID, int RoomNumber);

        // Update
        Task<HotelRoom> UpdateHotelRoom(int HotelID, int RoomNumber, HotelRoom hotelroom);

        // Delete 

        Task Delete(int HotelID, int RoomNumber);
    }
}
