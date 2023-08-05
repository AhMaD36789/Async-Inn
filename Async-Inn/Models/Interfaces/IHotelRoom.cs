namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelroom, int HotelID);

        // GET All
        Task<List<HotelRoomDTO>> GetHotelRooms(int HotelID);

        // GET Hotel By Id

        Task<HotelRoomDTO> GetHotelRoom(int HotelID, int RoomNumber);

        // Update
        Task<HotelRoomDTO> UpdateHotelRoom(int HotelID, int RoomNumber, HotelRoomDTO hotelroom);

        // Delete 

        Task Delete(int HotelID, int RoomNumber);
    }
}
